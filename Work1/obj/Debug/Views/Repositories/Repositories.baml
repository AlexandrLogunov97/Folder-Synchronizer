   M S B A M L   `   `   `  ����@  <Work1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Work1.Views.RepositoriesWork1.Views.Repositories  5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5      +    %Work1.Views.Repositories.Repositories   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:Work1.Views.Repositories     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5        N� Title$  Repositories��5
       N� 
Background
 ����� � 	Resources 5       �� 
TargetType �� Property �� Value �� 
TargetType ��Name �Name	 � ColumnDefinitions
 � RowDefinitions �� Triggers P� Property P� Value �� 
TargetName %� IsVirtualizing � UseLayoutRounding �� PanningRatio �� PanningDeceleration � OverridesDefaultStyle K� IsItemsHost   listTemplate �� Path��%  '��       '��     &	  )    �� 5   
   " ��6   �5      �� 5       � �� 5      �� 5      " ��6*   .R�5      �� $ border��6%   $��50��65   $��Stretch=�6A   ._�5   "   � $
 Name��6'   	 5   &   �� 5   *   �� 5   *   �� 5   *   $
��auto �6;   .+�5   &   � $��0��6+   
 5   *   �� 5   .   �� 5   .   .+�5   *   �� $��0��60   $��Stretch=�6X   $��Center=�6t   ��6=   �� *Source+6�   �� 5   *   $��1��60   $��Stretch=�6\   $��Center=�6x   ��6=   �� *
SourceType+6�   5   '   � 5    &   $��1��6+   
 5!   *   �� 5"   .   �� 5#   .   .+�5%   *   �� $��0��60   $��Stretch=�6X   $��Center=�6t   ��6=   �� *Target+6�   �� 5&   *   $��1��60   $��Stretch=�6\   $��Center=�6x   ��6=   �� *
TargetType+6�   5'   '   �� 5(   &   $��2��6,   ��6<   �� *
LastSync+6[   5)   #   5*       5+      P� 5,   "    �@��
IsSelected . . �5-   &   �� $ border��6-    � �� �#e9ebef6i   5/   #   51      53      55      �� 56   
   " ��6   �57      �� 58       � �� 59      �� 5:      " ��6*   .R�5;      �� #��z��6%   ._�5<   "   �� ��.   .  . $	 0.1Q�6�   $ 0.0001Q�6�    . .��5=   &   ��  . 6F   5>   #   5?      5@      5B      5D      �� 5E   
   .R�5F      � ���	#FFFF1515$	��200��6*   	 5G      �� 5H      �� 5I      .+�5K      �� $��0��6   ��6(   �� $
 Name�6H   �� 5L      $��1��6   6*   5M      5N      5O      .��5P      � 
 5Q   
   �� 5R      $
��auto �6   �� 5S      .+�5U   
   �� $��0��6   $��0,0,0,1q�6c   ��6   ��  �� ElementName$ DerictorySearch��$ BorderBrush�._�5V      � 	 5W      �� 5X      �� 5Y      $��40 �6'   
 5[      �� 5\      6&   .+�5^      �� -   5         ��Name$ DerictorySearch��5^   F   $��2��6   
�������$��5,5,5,5q�6_   $��30��6p   $��Center=�6|   $��16(�6�   $��0,0,30,0q�6�   .��6�   .  (System.Windows.Interactivity.Interaction   Triggers 5_      /  )System.Windows.Interactivity.EventTrigger  5`         	EventName$ KeyDown��6)   .  (System.Windows.Interactivity.TriggerBase   Actions. 5a      6  0System.Windows.Interactivity.InvokeCommandAction     Command 5b      �� *Search+   CommandParameter 68   �� $ DerictorySearch��$
 Text�5c        5d      $ KeyUp��6)   . 5e         5f      �� *Search+ 68   �� $ DerictorySearch��$
 Text�5g      .��5h   .   5i      �� 5j      $��30��6   $��30��6$   $��1��6/   $0��*/Work1;component/Content/Images/search.png��6@   6w   5k      5l      � 5m   
   $��1��6   	 5n      �� 5o      �� 5p      .+�5r      � $��2��6   
 5s      �� 5t      �� 5u      $
��auto �6$   .+�5w      �� ��6   �� *Repositories+5x      � 5y      $��1��6   	 5z      �� 5{      $
��auto �6+   �� 5|      �� 5}      .+�5      � $��0��6   	 5�      �� 5�      $	��100 �6/   �� 5�      �� 5�      .+�5�      �� $��30��6!   $��5q�6-   .��68   Delete6@   5�      5�      5�      5�      5�      5�      