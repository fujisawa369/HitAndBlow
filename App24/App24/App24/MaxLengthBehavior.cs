using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App24
{
    public class MaxLengthBehavior : Behavior<Entry>
    {
        /// <summary>
        /// 入力制限文字数
        /// </summary>
        public static int MaxLength { get; set; } = 100;

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

            // 入力制限文字数内なら OK
            if (args.NewTextValue.Length <= MaxLength)
            {
                return;
            }
            else
            {
                ((Entry)sender).Text = args.OldTextValue;
                
                return;
            }
        }
    }
}
