using System;
using System.Linq;
using System.Text;

namespace multilingualencoderlib
{
    public interface IEncoder
    {
        string Encode(string text);
        string Decode(string encoded);

        bool IsValidEncoding(string text);
    }

    public class Encod3r : IEncoder
    {
        public static Encoding[] GetEncodings() => Encoding.GetEncodings()
                                                        .Select(info => Encoding.GetEncoding(info.Name))
                                                        .OrderBy(enc => enc.HeaderName)
                                                        .ToArray();

        public Encoding Encoding { get; }

        private Encoding m_defaultEncoding;

        public Encoding DefaultEncoding
        {
            get
            {
                if (m_defaultEncoding == null)
                    return Encoding.Default;

                return m_defaultEncoding;
            }
            set { m_defaultEncoding = value; }
        }



        public Encod3r(Encoding encoding)
        {
            this.Encoding = encoding;
        }

        public string Encode(string text)
        {
            return DefaultEncoding.GetString(Encoding.GetBytes(text));
        }

        public string Decode(string encoded)
        {
            return Encoding.GetString(DefaultEncoding.GetBytes(encoded));
        }

        public bool IsValidEncoding(string text)
        {
            return text.Equals(Decode(Encode(text)));
        }

        public string BinaryString(string text)
        {
            return string.Join(" ",
                            Encoding.GetBytes(text)
                                    .Select(b =>
                                            Convert.ToString(b, 2).PadLeft(8, '0'))
                                                .ToArray());
        }

        public string Hex(string text)
        {
            return string.Join(" ",
                            Encoding.GetBytes(text)
                                    .Select(b =>
                                                b.ToString("X2"))
                                                .ToArray());
        }
    }

}
