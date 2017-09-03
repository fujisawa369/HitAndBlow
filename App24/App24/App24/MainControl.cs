using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.ComponentModel;
using System.Windows.Input;

namespace App24
{
    class MainControl : INotifyPropertyChanged
    {
        #region バッキングフィールド
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand GameCommand { set; get; }
        private string _textNumber;
        private string _answer = "456";
        private string _log;
        private int _trial = 1;
        #endregion

        #region バインディングターゲット

        /// <summary>
        /// メイン入力フォームの値セット
        /// </summary>
        public string TextNumber
        {
            set
            {
                if (_textNumber != value)
                {
                    _textNumber = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs(_textNumber));
                    }
                }
            }
            get
            {
                return _textNumber;
            }
        }

        public string TextLog
        {
            set
            {
                if (_log != value)
                {
                    _log = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs(_log));
                    }
                }
            }
            get
            {
                return _log;
            }
        }

        #endregion

        #region 初期処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainControl()
        {
            CreateNumbers();
            GameCommand = new Command<Entry>(ExecutionGameCommand);
        }

        /// <summary>
        /// お題である三桁の数字を作成
        /// </summary>
        private void CreateNumbers()
        {
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            int[] ary2 = array.OrderBy(i => Guid.NewGuid()).ToArray();
            
            this._answer = ary2[1].ToString() + ary2[2].ToString() + ary2[3].ToString();
        }
        

        #endregion
        
        #region 関数

        private void ExecutionGameCommand(Entry entry)
        {
            entry.Focus();
            var response = ExamineNumber();
        }

        /// <summary>
        /// 三桁になったら調べる
        /// </summary>
        private bool ExamineNumber()
        {
            if (_textNumber.Length == 3)
            {
                CheckWhetherTheyAllMatch();
                return true;
            }
            return false;
        }

        /// <summary>
        /// すべて一致しているか調べる
        /// </summary>
        private void CheckWhetherTheyAllMatch()
        {
            if (_answer==_textNumber)
            {
                (new Page()).DisplayAlert("CONGRATULATION", "答えは「"+ _answer + "」でした。" +"\n" + TextLog.ToString() + "\n" + _trial + "回目で一致しました。","OK");
                CreateNumbers();
                TextNumber = "";
                TextLog = "";
                _trial = 0;
            }
            else
            {
                ExamineHit();
            }
        }

        /// <summary>
        /// ヒットを調べる
        /// </summary>
        private void ExamineHit()
        {
            int count = 3;
            int hit = 0;
            int blow = 0;
            string textNumber = _textNumber.ToString();
            string answer = _answer.ToString();

            for (int i = 0; i < count; i++)
            {
                if (textNumber[i] == answer[i])
                {
                    hit++;
                    textNumber = textNumber.Remove(i, 1);
                    answer = answer.Remove(i, 1);
                    count--;
                    i--;
                }
            }

            if (count == 1)
            {
                DisplayLog(hit, blow);
            }
            else
            {
                ExamineBlow(hit, blow, textNumber, answer);
            }
            
        }

        /// <summary>
        /// ブローを調べる
        /// </summary>
        /// <param name="hit">ヒット数</param>
        /// <param name="blow">ブロー数</param>
        /// <param name="textNumber">入力された値</param>
        /// <param name="answer">お題の値</param>
        private void ExamineBlow(int hit, int blow, string textNumber, string answer)
        {
            for (int i = 0; i < textNumber.Length; i++)
            {
                char s = textNumber[i];

                if (answer.IndexOf(s) != -1)
                {
                    blow++;

                }
            }

            DisplayLog(hit, blow);
        }

        /// <summary>
        /// 結果を表示する
        /// </summary>
        private void DisplayLog(int hit, int blow )
        {
            TextLog += _trial + "回目　入力値" + _textNumber + " = HIT" + hit + " : " + "BLOW" + blow + "\n";
            TextNumber = "";
            _trial++;
        }

        #endregion
    }
}
