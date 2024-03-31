using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen_2_fis_uretme
{
    public class RowDto
    {
        public int yCord { get; set; }
        public List<ResponseDto> Words { get; set; } = new List<ResponseDto>();

        public RowDto(int _yCord)
        {
            yCord = _yCord;
        }
    }
}
