using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Validation
{
    public interface IValidator<TYPE>
    {
        void Validate(TYPE type);
    }
    public interface IValidator<TYPE1, TYPE2>
    {
        void Validate(TYPE1 type1, TYPE2 type2);
    }
    public interface IValidator<TYPE1, TYPE2, TYPE3>
    {
        void Validate(TYPE1 type1, TYPE2 type2, TYPE3 type3);
    }
    public interface IValidator<TYPE1, TYPE2, TYPE3, TYPE4>
    {
        void Validate(TYPE1 type1, TYPE2 type2, TYPE3 type3, TYPE4 type4);
    }
}
