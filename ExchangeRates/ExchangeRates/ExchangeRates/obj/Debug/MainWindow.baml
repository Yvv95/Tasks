   M S B A M L   `   `   `  ����H  DExchangeRates, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ExchangeRatesExchangeRates  5   	   C&clr-namespace:ExchangeRates.PropertiesExchangeRates.Properties  5   	       ExchangeRates.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ExchangeRates     5   	   9config&clr-namespace:ExchangeRates.Properties     5   	   -   5        $��startWindow��5   	     � Title$  Курс валют��5
   	   $	��360��6   $��786.189��6)    � SizeToContent$ Height=�6Z    � 	Resources 5      ��%    5      .��5      � -   5         �Name$ 	inputGrid��5      $��0,0,2,0q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   5      �� 5      6   .+�5   
   �� $��0��6   $
��Left=�6!   $��8��6<   $��0,-3,0,0q�6G   $	��Top=�6Y   $	��776��6q   �� 5   
   $��0��6   $
��Left=�6!   $��11��6<   $��0,36,0,0q�6H   $	��Top=�6Z   $	��776��6r   � 5   
   -   5        $��valGrid��5      $��1��6   ���	#FF1E223B$	��400��6B   .+�5      �� -   5         ��Name$ valutes��5      $%��Избранные валюты��6%   $
��Left=�6@   $	��Top=�6[   $��
Wide Latin)�6s   $��18(�6�   
�������$	��205��6�   $
��Bold"�6�   ��6�   Z� 6�   �� 5      -   5         ��Name$ 
lastUpdate��5      $��Right=�6*   $��27��6F   $��Актуально на ��6R   $	��Top=�6g   ���	#FFB01212{�.  $��15(�6�    �� SelectionBrush 6�   Z� ��6�   Z� ��6�   Z� 6�   � 5      -   5        $��valutesPanel��5      $
��Left=�6.   $��	10,42,0,0q�6J   $	��Top=�6]   � 5      -   5        $��valutesGrid��5      $	��Top=�6&   5       5(      �� 5)   
   $��0��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   .+�5*      �� -   5        	 ��Name$	 
mainButton��5*      $��Button��6)   $
��Left=�6:   $	��Top=�6U   $��34��6m   $��26��6�   {�.  ��6x   Z� ��6�   Z� ��5+      �� 5,      
 �� ImageSource
 6!   ��  �� Path$ First�5.      �� 5/      -	   5        $	 diagramButton��5/      $�� ��6,   $
��Left=�67   $	��Top=�6R   $��32��6j   $��24��6u   ��6�   Z� ��6�   Z� ��50      �� 51      
 6!   �� $ Second�53      �� 54      -
   5        $	 savingsButton��54      $
��Left=�6,   $	��Top=�6G   $��31��6_   $��26��6    J� RenderTransformOrigin$ 0.161,0.423&�6�   ��6�   Z� ��6�   Z� ��55      �� 56      
 6!   �� $ Third�58      �� 59      -   5        $	 settingButton��59      $��Button��6,   $
��Left=�6=   $	��Top=�6X   $��33��6p   $��24��6�   ��6{   Z� ��6�   Z� ��5:      �� 5;      
 6!   �� $ Fourth�5=      5>      5?      5@      