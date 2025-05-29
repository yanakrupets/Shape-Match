using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Extensions;
using Scriptable_Objects;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Figure figurePrefab;
        [SerializeField] private Transform figureParent;
        [SerializeField] private Vector2 spawnXRange = new(-0.05f, 0.05f);
        [SerializeField] private float spawnTime = 0.2f;
        [SerializeField] private int barPlacesCount = 5;
        [SerializeField] private int matchSetsCount = 10;
    
        private const int MatchCount = 3;
    
        private FigureData _figureData;
        private EventManager _eventManager;
        private UIManager _uiManager;
        private GameCanvas _gameCanvas;
    
        private List<Figure> _figures;

        public void Construct(
            FigureData figureData, 
            EventManager eventManager, 
            UIManager uiManager, 
            GameCanvas gameCanvas)
        {
            _figureData = figureData;
            _eventManager = eventManager;
            _uiManager = uiManager;
            _gameCanvas = gameCanvas;
        
            _eventManager.OnFigureClick += HideFigure;
            _eventManager.OnShuffle += Shuffle;
        
            _figures = new List<Figure>();
        }

        private void OnDisable()
        {
            if (_eventManager != null)
            {
                _eventManager.OnFigureClick -= HideFigure;
                _eventManager.OnShuffle -= Shuffle;
            }
        }

        public void StartGame()
        {
            GenerateLevel(barPlacesCount);
        }

        private void GenerateLevel(int barPlacesCount)
        {
            _gameCanvas.Active(false);
            _uiManager.Open<GameCanvas>(barPlacesCount);
        
            for (var i = 0; i < matchSetsCount; i++)
            {
                var shape = GetRandomElement(_figureData.Shapes, out var shapeIndex);
                var color = GetRandomElement(_figureData.Colors, out var colorIndex);
                var icon = GetRandomElement(_figureData.Icons, out var iconIndex);
        
                var key = new FigureKey(shapeIndex, colorIndex, iconIndex);

                CreateFigures(MatchCount, key, shape, color, icon);
            }

            _figures.Shuffle();
            StartCoroutine(SpawnFigure());
        }

        private void HideFigure(Figure figure)
        {
            Destroy(figure.gameObject);
            _figures.Remove(figure);
        
            CheckGameOver();
        }

        private void CheckGameOver()
        {
            if (_figures.Count != 0) return;
            _uiManager.Open<ResultCanvas>(GameResult.Win);
        }

        private void Shuffle(IReadOnlyDictionary<FigureKey, int> figuresInBar)
        {
            _gameCanvas.Active(false);
            foreach (var figure in _figures)
            {
                figure.IsEnable = false;
                
                figure.Rigidbody2D.simulated = false;
                figure.Rigidbody2D.velocity = Vector2.zero;
                
                var position = new Vector2(Random.Range(spawnXRange.x, spawnXRange.y), figureParent.position.y);
                figure.transform.position = position;
                figure.transform.rotation = Quaternion.identity;
            }

            var index = 0;
            foreach (var (key, currentCount) in figuresInBar)
            {
                var shape = _figureData.Shapes[key.ShapeIndex];
                var color = _figureData.Colors[key.ColorIndex];
                var icon = _figureData.Icons[key.IconIndex];
            
                var count = MatchCount - currentCount;
                while (count > 0)
                {
                    _figures[index].Initialize(key, shape, color, icon);
                    index++;
                    count--;
                }
            }

            for (; index < _figures.Count;)
            {
                var shape = GetRandomElement(_figureData.Shapes, out var shapeIndex);
                var color = GetRandomElement(_figureData.Colors, out var colorIndex);
                var icon = GetRandomElement(_figureData.Icons, out var iconIndex);
                
                var key = new FigureKey(shapeIndex, colorIndex, iconIndex);

                for (var i = 0; i < MatchCount; i++, index++)
                {
                    _figures[index].Initialize(key, shape, color, icon);
                }
            }
            
            _figures.Shuffle();
            StartCoroutine(SpawnFigure());
        }

        private void CreateFigures(int count, FigureKey key, Sprite shape, Color color, Sprite icon)
        {
            for (var j = 0; j < count; j++)
            {
                var position = new Vector2(Random.Range(spawnXRange.x, spawnXRange.y), figureParent.position.y);
                var figure = Instantiate(figurePrefab, position, Quaternion.identity, figureParent);
                figure.Initialize(key, shape, color, icon);
                figure.SetEventManager(_eventManager);
                _figures.Add(figure);
            }
        }
        
        private T GetRandomElement<T>(IReadOnlyList<T> collection, out int index)
        {
            index = Random.Range(0, collection.Count);
            return collection[index];
        }

        private IEnumerator SpawnFigure()
        {
            foreach (var figure in _figures)
            {
                figure.Rigidbody2D.simulated = true;
                yield return new WaitForSeconds(spawnTime);
            }
            
            _gameCanvas.Active(true);
            foreach (var figure in _figures)
            {
                figure.IsEnable = true;
            }
        }
    }
}
