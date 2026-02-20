namespace SquadApplication.AnimationCustom;

public class CustomsAnimation
{
    public async Task RadarScanAnimation(VisualElement element)
    {
        // Пульсирующий эффект 
        var pulseAnimation = new Animation
            {
                { 0, 0.5, new Animation(v => element.Scale = v, 1, 1.05) },
                { 0.5, 1, new Animation(v => element.Scale = v, 1.05, 1) }
            };

        // Вращение 
        var rotationAnimation = new Animation(v => element.Rotation = v, 0, 360);

        // Запускаем параллельно
        pulseAnimation.Commit(element, "RadarPulse", 16, 2000, Easing.SinInOut,
            (v, c) => element.Scale = 1, () => true);

        rotationAnimation.Commit(element, "RadarRotation", 16, 4000, Easing.Linear,
            (v, c) => element.Rotation = 0, () => true);
    }

    // 2. Анимация "Целеуказатель" 
    public async Task TargetLockAnimation(Button button)
    {
        // Сохраняем  цвета
        var originalTextColor = button.TextColor;
        var originalBorderColor = button.BorderColor;

        // Мигание красным 
        var blinkAnimation = new Animation
            {
                { 0, 0.2, new Animation(v => button.TextColor =
                    Color.FromRgba(255, 0, 0, v), 0, 1) },
                { 0.2, 0.4, new Animation(v => button.TextColor =
                    Color.FromRgba(255, 0, 0, v), 1, 0) },
                { 0.4, 0.6, new Animation(v => button.TextColor =
                    Color.FromRgba(255, 0, 0, v), 0, 1) },
                { 0.6, 0.8, new Animation(v => button.TextColor =
                    Color.FromRgba(255, 0, 0, v), 1, 0) },
                { 0.8, 1, new Animation(v => button.TextColor = originalTextColor, 0, 1) }
            };

        // Вибрация 
        await button.TranslateToAsync(-5, 0, 50, Easing.SpringOut);
        await button.TranslateToAsync(5, 0, 50, Easing.SpringOut);
        await button.TranslateToAsync(-3, 0, 50, Easing.SpringOut);
        await button.TranslateToAsync(3, 0, 50, Easing.SpringOut);
        await button.TranslateToAsync(0, 0, 50, Easing.SpringOut);

        // Мигание
        blinkAnimation.Commit(button, "TargetLockBlink", 16, 1000);
    }

    // 3. Анимация 
    public async Task SquadReadyAnimation(StackLayout form)
    {
        // Последовательное появление элементов как построение взвода
        var children = form.Children;

        foreach(var child in children)
        {
            if(child is VisualElement visualChild)
            {
                visualChild.Opacity = 0;
                visualChild.TranslationY = 20;

                // Анимация "выскакивания" как по команде
                await Task.Delay(100); // Задержка между "бойцами"

                await Task.WhenAll(
                    visualChild.FadeToAsync(1, 300, Easing.CubicOut),
                    visualChild.TranslateToAsync(0, 0, 400, Easing.SpringOut)
                );
            }
        }
    }

    // 4. Анимация "Перезарядка" для ProgressBar/ActivityIndicator
    public async Task ReloadAnimation(VisualElement element)
    {
        // Имитация перезарядки оружия
        await element.RotateToAsync(90, 200, Easing.CubicIn);
        await element.FadeToAsync(0.3, 100);
        await Task.Delay(300); // Пауза как смена магазина
        await element.FadeToAsync(1, 100);
        await element.RotateToAsync(0, 200, Easing.SpringOut);
    }

    // 5. Анимация "Сигнал рации" для уведомлений
    public async Task RadioSignalAnimation(Label label)
    {
        var originalColor = label.TextColor;

        var signalAnimation = new Animation
            {
                // Первый "бип"
                { 0, 0.1, new Animation(v => label.Scale = v, 1, 1.2) },
                { 0.1, 0.2, new Animation(v => label.Scale = v, 1.2, 1) },
                { 0, 0.2, new Animation(v => label.TextColor =
                    Color.FromRgba(0, 255, 0, v), 0, 1) },
                
                // Пауза
                { 0.2, 0.4, new Animation(v => label.TextColor = originalColor, 1, 1) },
                
                // Второй "бип"
                { 0.4, 0.5, new Animation(v => label.Scale = v, 1, 1.15) },
                { 0.5, 0.6, new Animation(v => label.Scale = v, 1.15, 1) },
                { 0.4, 0.6, new Animation(v => label.TextColor =
                    Color.FromRgba(0, 255, 0, v), 0, 1) },
                
                // Длинный сигнал
                { 0.6, 0.8, new Animation(v => label.Scale = v, 1, 1.3) },
                { 0.8, 1, new Animation(v => label.TextColor = originalColor, 1, 1) }
            };

        signalAnimation.Commit(label, "RadioSignal", 16, 2000);
        label.Scale = 1;
    }

    // 6. Анимация "Взрыв/Попадание" для неправильного ввода
    public async Task HitMarkerAnimation(VisualElement element)
    {
        // Эффект попадания
        await element.ScaleToAsync(1.3, 100, Easing.CubicIn);
        element.BackgroundColor = Color.FromRgba(255, 0, 0, 0.3);

        // Дрожание
        var shakeAnimation = new Animation();
        for(int i = 0; i < 5; i++)
        {
            int direction = (i % 2 == 0) ? -5 : 5;
            shakeAnimation.Add(i * 0.2, (i + 1) * 0.2,
                new Animation(v => element.TranslationX = v, 0, direction));
        }

        shakeAnimation.Commit(element, "HitShake", 16, 500);

        // Возврат
        await Task.WhenAll(
            element.ScaleToAsync(1, 200, Easing.SpringOut),
            element.TranslateToAsync(0, 0, 200, Easing.SpringOut)
        );

        // Плавное исчезновение красного фона
        var fadeOut = new Animation(v => element.BackgroundColor =
            Color.FromRgba(255, 0, 0, v), 0.3, 0);
        fadeOut.Commit(element, "HitFade", 16, 500);
    }

    // 7. Анимация "Ночное видение" для переключения тем
    public async Task NightVisionToggleAnimation(ContentPage page)
    {
        // Эффект включения ПНВ
        var overlay = new BoxView
        {
            BackgroundColor = Color.FromRgba(0, 255, 0, 0),
            Opacity = 0
        };

        // Добавляем поверх всего
        if(page.Content is Layout layout)
        {
            layout.Children.Add(overlay);
        }

        // Анимация "щелчка" и зеленого свечения
        await overlay.FadeToAsync(0.1, 500, Easing.CubicIn);

        // Статический шум (быстрое изменение прозрачности)
        var noiseAnimation = new Animation();
        Random rnd = new Random();

        for(int i = 0; i < 10; i++)
        {
            double opacity = 0.05 + (rnd.NextDouble() * 0.1);
            noiseAnimation.Add(i * 0.1, (i + 1) * 0.1,
                new Animation(v => overlay.Opacity = v,
                    overlay.Opacity, opacity));
        }

        noiseAnimation.Commit(overlay, "NightVisionNoise", 16, 1000);

        // Плавное выключение
        await overlay.FadeToAsync(0, 300, Easing.CubicOut);

        if(page.Content is Layout finalLayout)
        {
            finalLayout.Children.Remove(overlay);
        }
    }

    // 8. Анимация "Снайперская метка" для фокуса
    public async Task SniperDotAnimation(Entry entry)
    {
        // Создаем красную точку как прицел
        var dot = new Border
        {
            BackgroundColor = Colors.Red,
            StrokeThickness = 10,
            WidthRequest = 10,
            HeightRequest = 10,
            Scale = 0,
            Opacity = 0,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 0, 10, 0)
        };

        // Добавляем к Entry
        if(entry.Parent is Layout parentLayout)
        {
            parentLayout.Children.Add(dot);

            // Анимация появления точки
            await dot.ScaleToAsync(1, 300, Easing.SpringOut);
            await dot.FadeToAsync(1, 200);

            // Пульсация точки
            var pulse = new Animation
                {
                    { 0, 0.5, new Animation(v => dot.Scale = v, 1, 1.5) },
                    { 0.5, 1, new Animation(v => dot.Scale = v, 1.5, 1) }
                };

            // 3 пульсации
            for(int i = 0; i < 3; i++)
            {
                pulse.Commit(dot, $"DotPulse{i}", 16, 500);
            }

            // Исчезновение
            await dot.FadeToAsync(0, 200);
            parentLayout.Children.Remove(dot);
        }
    }

    // 9. Анимация "Тактическая пауза" между действиями
    public async Task TacticalPauseAnimation(VisualElement element, int pauseMs = 500)
    {
        // Мигание как индикатор готовности
        await element.FadeToAsync(0.5, 100);
        await Task.Delay(pauseMs / 2);
        await element.FadeToAsync(1, 100);
        await Task.Delay(pauseMs / 2);
        await element.FadeToAsync(0.5, 100);
        await element.FadeToAsync(1, 100);
    }

    // 10. Комплексная анимация "Миссия выполнена" для успешной авторизации
    public async Task MissionAccomplishedAnimation(ContentPage page)
    {
        // Зеленый экран успеха
        var successOverlay = new BoxView
        {
            BackgroundColor = Color.FromRgba(0, 255, 0, 0),
            Opacity = 0
        };

        if(page.Content is Layout layout)
        {
            layout.Children.Add(successOverlay);

            // Вспышка зеленого
            await successOverlay.FadeToAsync(0.3, 200);

            // Анимация "чекмарк"
            var checkmark = new Label
            {
                Text = "✓",
                FontSize = 80,
                TextColor = Colors.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Scale = 0,
                Rotation = -45
            };

            layout.Children.Add(checkmark);

            // Анимация появления галочки
            await Task.WhenAll(
                checkmark.ScaleToAsync(1, 500, Easing.SpringOut),
                checkmark.RotateToAsync(0, 500, Easing.SpringOut),
                checkmark.FadeToAsync(1, 300)
            );

            // Текст успеха
            var successText = new Label
            {
                Text = "ДОСТУП РАЗРЕШЁН",
                FontSize = 24,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                TranslationY = 100,
                Opacity = 0
            };

            layout.Children.Add(successText);

            // Появление текста
            await Task.WhenAll(
                successText.FadeToAsync(1, 500),
                successText.TranslateToAsync(0, 120, 500, Easing.SpringOut)
            );

            // Задержка и очистка
            await Task.Delay(1000);

            await Task.WhenAll(
                successOverlay.FadeToAsync(0, 500),
                checkmark.FadeToAsync(0, 500),
                successText.FadeToAsync(0, 500)
            );

            layout.Children.Remove(successOverlay);
            layout.Children.Remove(checkmark);
            layout.Children.Remove(successText);
        }
    }
}


