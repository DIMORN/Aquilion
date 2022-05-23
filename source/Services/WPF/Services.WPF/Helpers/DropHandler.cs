

namespace Services.WPF.DragDrop
{
    public class DropHandler : IDropTarget
    {
        private static IDropTarget _instance;
        public static IDropTarget Instnace => _instance ??= new DropHandler();
        public void DragOver(IDropInfo dropInfo)
        {
            var debugwindow = Application.Current.Windows.OfType<DebugWindow>().FirstOrDefault();

            if (dropInfo.Data is DataObject dataObject && dataObject.GetFileDropList().Count != 0)
            {
                if (debugwindow != null)
                {
                    debugwindow.TextBlock.Text = $"Data: {dataObject.GetFileDropList()[0]}\nTargetCollection: {dropInfo.TargetCollection}\nTarget Item: {dropInfo.TargetItem}";

                    dropInfo.Effects = DragDropEffects.Move;
                    dropInfo.EffectText = $"Go to ";
                    dropInfo.DestinationText = $"{dataObject.GetFileDropList()[0]}";
                }

            }
            else
            {
                if (debugwindow != null)
                {
                    debugwindow.TextBlock.Text = $"Data: {dropInfo.Data}\nTargetCollection: {dropInfo.TargetCollection}\nTarget Item: {dropInfo.TargetItem}";
                    dropInfo.Effects = DragDropEffects.Move;
                    dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                    dropInfo.EffectText = $"Move to";
                    dropInfo.DestinationText = $"{dropInfo.TargetCollection}";
                }

            }
        }
        public void Drop(IDropInfo dropInfo)
        {

        }
    }
}
