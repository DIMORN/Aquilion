using System;
using System.IO;
using GongSolutions.Wpf.DragDrop;

namespace ExplorerApp.Helpers;

internal class DropHandler : IDropTarget
{
    private static IDropTarget _instance;
    public static IDropTarget Instnace => _instance ??= new DropHandler();
    public void DragOver(IDropInfo dropInfo)
    {

        var o = dropInfo.Data as DataObject;
        var d = o.GetFileDropList();
        if(d.Count == 1)
        {
            dropInfo.EffectText = $"Drop to go to {d[0]}";
            dropInfo.Effects = DragDropEffects.Scroll;
            var attr = File.GetAttributes(d[0]);
            if (attr.HasFlag(FileAttributes.Directory))
            {
                var di = new DirectoryInfo(d[0]);
                
            }
            else if (attr.HasFlag(FileAttributes.Normal))
            {
                var fi = new FileInfo(d[0]);
            }
            else if (attr.HasFlag(FileAttributes.Device))
            {
                var drv = new DriveInfo(d[0]);
            }
        }
    }

    public void Drop(IDropInfo dropInfo)
    {
        if (dropInfo.VisualTarget is ExplorerApp.Controls.ShellView shv)
        {
            ((ViewModels.MainViewModel)shv.DataContext).ExplorerViewModel.OnOpen(new Shared.Core.Library.FileSystem.DirectoryModel(new DirectoryInfo((dropInfo.Data as DataObject).GetFileDropList()[0])));
        }
    }
}
