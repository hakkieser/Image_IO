using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Image_IO.Core
{
    public interface I_ImageCollectionSaver
    {
        List<string> Save(SaveModel _saveModel);         
    }
}
