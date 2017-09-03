using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App24
{
    class DuplicationCheckBehavior : Behavior<Entry>
    {
        /// <summary>
        /// 入力制限重複不可
        /// </summary>
        public static bool Check { get; set; }

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
        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (string.IsNullOrEmpty(args.NewTextValue)) { return; }

            var hashset = new HashSet<char>();

            string str = ((Entry)sender).Text;

            foreach (var item in str)
            {
                if (!hashset.Add(item))
                {
                    ((Entry)sender).Text = args.OldTextValue;
                }
            }
        }
    }
}
