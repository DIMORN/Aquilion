using System.IO;
using Prism.Mvvm;

namespace Controls
{
    public class MediaFile : BindableBase
    {
        public string Name { get; set; }
        public string FullFileName { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }
        public byte[] Cover { get; set; }

        public MediaFile(string fullname)
        {
            var file = new FileInfo(fullname);
            FullFileName = file.FullName;
            var tag = TagLib.File.Create(file.FullName).Tag;
            Name = tag.Title;
            Artist = tag.FirstPerformer;
            Album = tag.Album;
            Genre = tag.FirstGenre;
            Year = tag.Year.ToString();
            Cover = tag.Pictures[0].Data.Data;
        }
    }
}