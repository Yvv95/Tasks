   M S B A M L   `   `   `  ����H  DExchangeRates, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ExchangeRatesExchangeRates  5          ExchangeRates.DynamicForm   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ` \Xceed.Wpf.Toolkit, Version=3.3.17121.18230, Culture=neutral, PublicKeyToken=ba83ff368b7563c64xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      'localclr-namespace:ExchangeRates  5      .��5      � -   5        $��dynamicGrid��5      $��Stretch=�5      ���	#FF1E223B$	��300��5        � RowDefinitions  5   
   �� 5      $��50 �6   5      �� 5      6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�5      $��
20, 0, 0,0q�6.   .+�5      �� $#��Выберите валюту��6   $��30��5      $
��Auto��5      
�������$��Brush Script MT)�5      $��14(�5      $
��Bold"�5      $
��Left=�5      ��5      Z� 5   1   �� 5      -   5        $��	selectBox��5      $��25��5       $
��Auto��5!      $
��Left=�5"         ComboBoxStyle2#���   5$       �� Text$ Валюта��5%      ��5#      ��  �� Path$ allList�5&   G   �� 5'      $��и период��6   $��30��5(      $
��Auto��5)      
�������$
��Left=�5+      $��Brush Script MT)�5,      $��14(�5-      $
��Bold"�50      ��5.      Z� 50   (   �� 51      -   5        $��	periodBox��51      $ Период��52      #���   53      $��25��54      56      57      #  Xceed.Wpf.Toolkit.Chart.Chart  58   
   -   5        $��graphicChart��58      
�������$��1��5:         
ShowLegend .    Legend 5;      $  Xceed.Wpf.Toolkit.Chart.Legend  5<      -   5        $��legenda��5C   8      Dock$
 left=�5<         AllowResize .    	AllowDock . 	   	AllowMove	 . 
   Title$
  ��5@      $��0q�5A      
������ 
������ 5C   8      Areas 5E      "  Xceed.Wpf.Toolkit.Chart.Area  5F      -   5          Name$ _area1��5F         Title$ Даты��6,      XAxis 5H      "  Xceed.Wpf.Toolkit.Chart.Axis  5I         ShowTickLabels .     ShowAxisLabel .    
LabelsType$ DateTime=�5K   $      GraduationMode$ Manual=�5L   $   $	��200��5M   $      Title$ Дата��5N   $      	ShowArrow .    ShowAxis .    ShowGridLines .    	ShowTicks .    TitleMargin$ 10Q�5S   $   $��stretch��5T   $      AxisLabelsLayout$ 	ShowToFit=�5U   $   $��1q�5V   $   
���   �$��Italic$�5X   $      YAxis 5[        5\       .  . $ 	ShowToFit=�5^   $   $	��200��5_   $   $ Курс��5`   $   $ 10Q�5a   $      Series 5e      $  Xceed.Wpf.Toolkit.Chart.Series  5h      -   5        _series1��   DefaultInterior$ White��5i   &      Title$ Обозначения��5j   &      HintLineLength$ 20Q�5k   &      ShowPointsInLegend .     ShowHintLabels  . !   Layout! 5o      (  "Xceed.Wpf.Toolkit.Chart.LineLayout  5p   "   "   
DataPoints" 5r      5u      5{      5}      5      5�      