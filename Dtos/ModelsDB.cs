using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcingYamaha.Dtos
{
    public class ModelsDB
    {
        public int Id { get; set; }
        public string prodPictureFileURL { get; set; }
        public string? nickname { get; set; }
        public string modelName { get; set; }
        public string modelYear { get; set; }
        public string? modelTypeCode { get; set; }
        public string? productNo { get; set; }
        public string? colorType { get; set; }
        public string? colorName { get; set; }

    }
}
