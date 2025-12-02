using SquadApplication.Models;
using System.Collections.ObjectModel;

namespace SquadApplication.ViewModels;

public partial class PolygonsViewModel : ObservableObject
{
    public PolygonsViewModel()
    {
        polygons = new ObservableCollection<PolygonEntity>(PolygonEntity.GetTestData());
    }

    [ObservableProperty]
    private ObservableCollection<PolygonEntity> polygons ;


}
