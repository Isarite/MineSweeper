using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace MineServer.Models
{
    public class PlayerDataList: IEnumerable<string>
    {
        private static PlayerList _playerList;
        private static HighscoreList _highscoreList;
        private static VeteranPlayerList _veteranPlayerList;

        public PlayerDataList(IList<Player> players, IList<Game> games)
        {
            _playerList = new PlayerList(players);
            _highscoreList = new HighscoreList(players, games);
            _veteranPlayerList = new VeteranPlayerList(players, games);
        }

        public void BuildData()
        {
            _playerList.BuildList();
            _highscoreList.BuildList();
            _veteranPlayerList.BuildList();
        }

        public IEnumerator<string> GetEnumerator()
        {
            return new PlayerDataEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public sealed class PlayerDataEnumerator : IEnumerator<string>
        {
            private string _current;
            private IEnumerator<string> _currentEnumerator;
            private int _number = 0;

            public bool MoveNext()
            {
                if (_current == null && _number == 0)
                {
                    _currentEnumerator = _playerList.Lines.GetEnumerator();
                }

                var result = _currentEnumerator.MoveNext();
                _current = _currentEnumerator.Current;
                if (!result)
                    switch (_number)
                    {
                        case 0:
                            _currentEnumerator = _highscoreList.Lines.GetEnumerator();
                            _number++;
                            return MoveNext();
                        case 1:
                            _currentEnumerator = _veteranPlayerList.Lines.GetEnumerator();
                            _number++;
                            return MoveNext();
                        case 2:
                            return false;
                    }

                return result;
            }

            public void Reset()
            {
            }

            string IEnumerator<string>.Current => _current;

            public object Current => _current;

            public void Dispose()
            {
            }
        }
    }
}

