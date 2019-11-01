using mle_app.Common;
using multilingualencoderlib;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace mle_app.Models
{
    public class EncoderModel : NotifyBase
    {

        private readonly CommandBindingCollection _CommandBindings = new CommandBindingCollection();
        public CommandBindingCollection CommandBindings
        {
            get
            {
                return _CommandBindings;
            }
        }

        private multilingualencoderlib.Encod3r m_encoder;

        public EncoderModel(Encoding encoding) : this(encoding.HeaderName) { }

        public EncoderModel(string encoding)
        {
            this.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "EncodingName")
                {
                    m_encoder = new Encod3r(Encoding.GetEncoding(EncodingName));
                }

                if (e.PropertyName == "Text")
                {
                    EncodedText = m_encoder.Encode(Text);
                    Binary = m_encoder.BinaryString(Text);
                    Hex = m_encoder.Hex(Text);
                    IsValid = m_encoder.IsValidEncoding(Text);

                    var encTextLen = EncodedText.Length;
                    var txtLen = Text.Length;

                    ByteCount = txtLen > 0 ? encTextLen / txtLen : 0;
                }
            };
            EncodingName = encoding;

            _CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, (_, __) =>
           {
               Clipboard.SetText(EncodedText, TextDataFormat.UnicodeText);
           }));
        }

        public bool IsValid
        {
            get
            {
                return Get<bool>();
            }
            set
            {
                Set(value);
            }
        }

        public string Text
        {
            get
            {
                return Get<string>();
            }
            set
            {
                Set(value);
            }
        }

        public string EncodingName
        {
            get
            {
                return Get<string>();
            }
            set
            {
                Set(value);
            }
        }

        public string EncodedText
        {
            get
            {
                return Get<string>();
            }
            set
            {
                Set(value);
            }
        }

        public string Binary
        {
            get
            {
                return Get<string>();
            }
            set
            {
                Set(value);
            }
        }

        public string Hex
        {
            get
            {
                return Get<string>();
            }
            set
            {
                Set(value);
            }
        }

        public int ByteCount
        {
            get
            {
                return Get<int>();
            }
            set
            {
                Set(value);
            }
        }


    }
}
