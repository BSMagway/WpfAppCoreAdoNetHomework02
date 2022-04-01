using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppAdoNetHomework02.Model
{
    public class CompactDisc
    {
        public int Id { get; set; }
        public string AlbumName { get; set; }
        public int SingerOrGroupNameId { get; set; }
        public SingerOrGroupName SingerOrGroupName { get; set; }
        public int GenreId  { get; set; }
        public Genre Genre { get; set; }

        public List<CompactDiscOnWarehouse> CompactDiscOnWarehouses { get; set; } = new List<CompactDiscOnWarehouse>();

        public CompactDisc()
        {

        }
        public CompactDisc(string name, int singerOrGroupNameId, int genreId)
        {
            AlbumName = name;
            SingerOrGroupNameId = singerOrGroupNameId;
            GenreId = genreId;
        }
    }
}
