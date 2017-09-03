using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App24
{
    public class RegexBehavior : Behavior<Entry>
    {
        /// <summary>
        /// 正規表現
        /// </summary>
        private Regex _reg;

        /// <summary>
        /// 正規表現パターン
        /// </summary>
        public string RegexPattern { get { return _reg?.ToString(); } set { _reg = new Regex(value); } }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        /// <summary>
        /// 入力テキスト変更時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue)) { return; }

            // 正規表現にマッチする場合 OK
            if (_reg.IsMatch(e.NewTextValue))
            {
                return;
            }
            else
            {
                ((Entry)sender).Text = e.OldTextValue;
                return;
            }
        }
    }
}
