   M S B A M L   `   `   `  ����  ConwaysGameOfLife.UI%    ConwaysGameOfLife.UI.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Conway's Game of Life��5   	   $	��700��6'   $	��650��64   
�������.��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��30 �6   .+�5   
   �� -   5        $��PlayingField��5      $��0��6   $��Stretch=�62   $��Stretch=�6P   ���	#000000006l   �� 5   
   -   5        $��UserInputLayer��5      $��0��6   $��Stretch=�64   $��Stretch=�6R   ���	#110000006�   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      �� 5      �� 5      �� 5      .+�5      �� -   5        $
��Tick��5   F   $��0��6   $
��Tick��6%   
����������	#FF0A0056
���\\��$
��Bold"�6�   $��14(�6�   �� 5      -   5        $��Start��5   H   $��1��6   $��Start��6%   ���	#FF0A0056
�������
���\\��$
��Bold"�6�   $��14(�6�   �� 5      -   5        $
��Stop��5   F   $��2��6   $
��Stop��6%   
����������	#FF0A0056
���\\��{�.  $
��Bold"�6�   $��14(�6�   �� 5      -   5        $��3��5      $��Clear��6%   
����������	#FF0A0056
���\\��$
��Bold"�6�   $��14(�6�   5      5      5      