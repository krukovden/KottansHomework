using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceasar.Tests
{
    public class CeasarCipher
    {
        public int offset { get; private set; }
        public int maxValue { get; set; }
        public int minValue { get; set; }

        public CeasarCipher(int offset)
        {
            this.offset = offset;
            maxValue = 126;
            minValue = 33;
        }

        public string Encrypt(string message)
        {
            if (message == null)
                throw new ArgumentNullException();

            if (String.IsNullOrEmpty(message))
                return message;

            char[] mes = message.ToCharArray();

            for (int i = 0; i < message.Length; i++)
                if (mes[i] == 32) continue;
                else
                if (minValue <= mes[i] && mes[i] <= maxValue)
                    mes[i] =
                        (char)
                            ((mes[i] + offset) <= maxValue
                                ? ((mes[i] + offset) <= minValue ? maxValue : (mes[i] + offset))
                                : ((mes[i] + offset) % maxValue + minValue - 1));
                else
                    throw new ArgumentOutOfRangeException(new char[] { mes[i] }.ToString());

            return new string(mes);

        }

        public string Decrypt(string message)
        {
            offset *= -1;
            string encoder = Encrypt(message);
            offset *= -1;
            return encoder;
        }
    }
}
