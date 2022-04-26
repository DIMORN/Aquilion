using System;
using Shared.Core.Library.Selectable;

namespace Shared.Core.Library.FileSystem
{
    public interface IFileSystemModel : ISelectable
    {
        public string? Name { get; set; }
        public string? FullName { get; set; }
        public string? Extension { get; set; }

    }
}
