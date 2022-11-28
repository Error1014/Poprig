using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSUniversalLib.dll
{
    public class Calculation
    {
        public int GetQuantityForProduct(int productType, int materialType, int count, float width, float length)
        {
            float koefProduct = GetKoefProduct(productType);
            float koefMaterial = GetKoefMaterial(materialType);
            if (koefProduct == -1) return -1;
            if (koefMaterial == -1) return -1;
            if (width>=0) return -1;
            if (length >= 0) return -1;
            float value = ((width * length) * koefProduct) + ((width * length) * koefProduct) * koefMaterial;
            value = value % 1 == 0 ? value : (value / 1 + 1);
            return (int)value;
        }

        private float GetKoefProduct(int productType)
        {
            if (productType == 1)
            {
                return 1.1f;
            }
            else if (productType == 2)
            {
                return 2.5f;
            }
            else if (productType == 3)
            {
                return 8.43f;
            }
            else
            {
                return -1;
            }
        }
        private float GetKoefMaterial(int materialType)
        {
            if (materialType == 1)
            {
                return 0.3f;
            }
            else if (materialType == 2)
            {
                return 0.12f;
            }
            else
            {
                return -1;
            }
        }
    }
}
