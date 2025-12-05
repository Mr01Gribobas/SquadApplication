namespace SquadApplication.AnimationCustom;

internal class CustomsAnimation
{
    public async void StartComplexAnimation(object itemFromPage)
    {
        var item = (CollectionView)itemFromPage;

        var animation = new Animation();

        // Анимация прозрачности
        animation.Add(0, 0.5, new Animation(v => item.Opacity = v, 0, 1));

        // Анимация перемещения по X
        animation.Add(0, 1, new Animation(v => item.TranslationX = v, 0, 200));

        // Анимация вращения
        animation.Add(0.5, 1, new Animation(v => item.Rotation = v, 0, 90));

        animation.Commit(item, "ComplexAnimation", 16, 2000);
    }
    private async void TEstAnimation(object elementPage)
    {
        var element = (Button)elementPage;
       // Линейная(равномерная)
         await element.TranslateTo(100, 0, 1000, Easing.Linear);

        // Плавное ускорение
        await element.TranslateTo(100, 0, 1000, Easing.SinIn);

        // Плавное замедление
        await element.TranslateTo(100, 0, 1000, Easing.SinOut);

        // Ускорение + замедление
        await element.TranslateTo(100, 0, 1000, Easing.SinInOut);

        // Пружинная анимация
        await element.TranslateTo(100, 0, 1000, Easing.SpringIn);
        await element.TranslateTo(100, 0, 1000, Easing.SpringOut);

        // Отскок
        await element.TranslateTo(100, 0, 1000, Easing.BounceIn);
        await element.TranslateTo(100, 0, 1000, Easing.BounceOut);

        // Резиновая (с перелетом)
        await element.TranslateTo(100, 0, 1000, Easing.CubicInOut);
    }
}
