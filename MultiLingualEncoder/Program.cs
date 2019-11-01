using System;
using System.IO;
using System.Linq;
using System.Text;

namespace MultiLingualEncoder
{
    class Program
    {
        static void Main(string[] args)
        {
            //string text = "（书、杂志等中区别于图片的）";
            //string text = "วรรณยุต";
            string text = "똠양꿍";

            using (var f = File.Create("txt.txt"))
            {
                using (var fw = new StreamWriter(f))
                    foreach (var enc in Encoder.GetEncodings())
                    {
                        var encoder = new Encoder(enc);

                        var encoded = encoder.Encode(text);
                        var decoded = encoder.Decode(encoded);
                        var binaryStr = encoder.BinaryString(text);
                        var hex = encoder.Hex(text);

                        if (encoder.IsValidEncoding(text))
                            fw.WriteLine(enc.HeaderName + ":\t\t\t\t\t\t\t\t" + encoded + " = " + decoded + "  -- " + binaryStr + " " + hex);
                    }

            }

        }
    }

    public interface IEncoder
    {
        string Encode(string text);
        string Decode(string encoded);

        bool IsValidEncoding(string text);
    }

    public class Encoder : IEncoder
    {
        public static Encoding[] GetEncodings() => Encoding.GetEncodings().Select(info => Encoding.GetEncoding(info.Name)).ToArray();

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



        public Encoder(Encoding encoding)
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
