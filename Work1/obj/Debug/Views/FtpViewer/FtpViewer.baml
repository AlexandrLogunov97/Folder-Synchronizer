   M S B A M L   `   `   `  ����@  <Work1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Work1.Views.FtpViewerWork1.Views.FtpViewer  5      < 8S, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null clr-namespace:S;assembly=SS 5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5      %    Work1.Views.FtpViewer.FtpViewer   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:Work1.Views.FtpViewer     5      (sclr-namespace:S;assembly=S     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5        N� Title$  	FtpViewer��5       � 	Resources 5       �� 
TargetType �� Property �� Value �� 
TargetType ��Name � ColumnDefinitions �� Path	 ��Name
 �� Triggers P� Property P� Value �� 
TargetName %� IsVirtualizing � UseLayoutRounding �� PanningRatio �� PanningDeceleration � OverridesDefaultStyle K� IsItemsHost   listTemplate  	Unenabled �� Triggers��%2  '��       '�� �    &	  �    &	 �    �� 5   
   " ��6   �5      �� 5       � �� 5      �� 5      " ��6*   .R�5      �� $ border��6%   $��50��65   $��Stretch=�6A   ._�5   "   �  5   &   �� 5   *   $
��auto �6;   �� 5   *   .+�5   &   �� $��0��6,   $��5q�6<   ��6G   �� $
 Icon�6f   �� 5   &   $
	 Name��6,   $��10,5,5,5q�6:   #��z��6M   #��z��6s   $��Center=�6�   $��1��6�   ��6�   �� $
 Name�6�   5   #   5      
 5      P� 5   "    �@��
IsSelected . . �5   &   �� $ border��6-    � �� �#e9ebef6i   �� 5   &   $
 Name��6-    � �� �#323a456g   5    #   5"      5$      5&      �� 5'   
   " ��6   �5(      �� 5)       � �� 5*      �� 5+      " ��6*   .R�5,      �� #��z��6%   ._�5-   "   �� ��.   .  . $	 0.1Q�6�   $ 0.0001Q�6�    . .��5.   &   ��  . 6F   5/   #   50      51      53      55      �� 56   
   .R�57      � ���	#FFFF1515$	��200��6*    58      �� 59      �� 5:      .+�5<      �� $��0��6   ��6(   �� $
 Name�6H   �� 5=      $��1��6   6*   5>      5?      �� 5@   
   " ��6"    5A      P� 5B       � {� .  . �5C      ��  � ��
 �����6G   5D      5F      5G      .��5H      � 
������� 5I   
   �� 5J      �� 5K       � RowDefinitions 5M   
   �� 5N      �� 5O      .+�5Q   
   �� $��2��6   $��0,0,1,0q�6g   ��6"   ��  �� ElementName$ DerictorySearch��$ BorderBrush�._�5R      � 
������� 5S      �� 5T      $
��auto �6$   �� 5U      �� 5V      $
��auto �6$   .+�5X      �� $��0��6   $��0,0,0,1q�6k   ��6&   �� $ DerictorySearch��$ BorderBrush�._�5Y      �  5Z      �� 5[      �� 5\      $��40 �6/    5^      �� 5_      6.   .+�5a      �� -   5         ��Name$ DerictorySearch��5a   N   $��2��6"   
�������$��5,5,5,5q�6g   $��30��6x   $��Center=�6�   $��16(�6�   $��0,0,30,0q�6�   .��6�   .  (System.Windows.Interactivity.Interaction   Triggers 5b      /  )System.Windows.Interactivity.EventTrigger  5c   "      	EventName$ KeyDown��61   .  (System.Windows.Interactivity.TriggerBase   Actions. 5d   &   6  0System.Windows.Interactivity.InvokeCommandAction     Command 5e      �� *Search+   CommandParameter 68   �� $ DerictorySearch��$
 Text�5f   #     5g   "   $ KeyUp��61   . 5h   &      5i      �� *Search+ 68   �� $ DerictorySearch��$
 Text�5j   #   .��5k   6   5l      �� 5m      $��30��6    $��30��6,   $��1��67   $0��*/Work1;component/Content/Images/search.png��6H   6   5n      5o      �� 5p      -   5         ��Name$ folders��5p   �   $��1��6   #��[  6^   
�������
���   �$��16(�6�    �� SelectedItem 6'   �� *SelectedDirectory+ �� Mode$ TwoWay=���6�   �� *Derictories+ 5q        5r      $ MouseDoubleClick��6)   . 5s         5t      �� *ToDirectory+5u      5w      � 5x      $��2��6    5y      �� 5z      $
��auto �6+   �� 5{      $
��auto �6+   �� 5|      $
��auto �6+   �� 5}      �� 5~      $
��auto �6+   .+�5�      �� $��Create��6     S.Navigation
    Uri$*  $Views/FtpViewer/CreateDerectory.xaml��6.   $��30��6f   $��5,5,5,5q�6r   $��0��6�   $��57��6�   �� 5�      $��Remove��6   $��30��6.   $��0,5,5,5q�6:   $��1��6f   $��57��6v   ��6K   �� *Delete+6v   �� 5�      $��Rename��6   $*  $Views/FtpViewer/RenameDerectory.xaml��6.   $��30��6�   $��0,5,5,5q�6�   $��2��6�   $��57��6�   ��6f   �� *SelectRenamedDerectory+6�   �� 5�      $��30��6:   $��0,5,5,5q�6F   $��4��6W   $��57��6g   ��6   �� *	Refresh+.��6r   Reload5�      5�      5�      5�      � 5�   
   $��1��6   $��2��6    5�      �� 5�      $
��auto �6    �� 5�      �� 5�      $
��auto �6    .+�5�      �� -   5        $ query��5�   #   $��0��6   $��5q�63   $��30��6h   $��Center=�6t   ! �� 
IsReadOnly! . ��6>   �� *SelectedDirectory.Adress+6�   � 5�      $��1��6    5�      �� 5�      .+�5�      �� $��0��6   $��Stretch=�6/   $��Stretch=�6T   $
��Auto=�6w   .��5�      �� .+�5�      �� .��6    Name6&   �� 5�      $��30��6"   $��5,0,5,0q�6.   {�.  $��Center=�6�   ��6?   �� *SelectedDirectory.DisplayName+6�   �� 5�      .��6    Size6&   �� 5�      $��30��6"   $��5,0,5,0q�6.   {�.  $��Center=�6}   ��6?   �� *SelectedDirectory.FileSize+6�   �� 5�      .��6    Date6&   �� 5�      $��30��6"   $��5,0,5,0q�6.   {�.  $��Center=�6y   ��6?   �� *SelectedDirectory.Date+6�   �� 5�      .��6    Description6-   �� 5�      $	��116��6"   $��5,0,5,0q�6/   " �� TextWrapping$
" Wrap=�6@   "# �� VerticalScrollBarVisibility$
# Auto=�6T   5�      5�      5�      � 5�      $��2��6    5�      �� 5�      �� 5�      �� 5�      .+�5�      �� $   IsGoBack$ . $��0��66   $��5q�6F   $��30��6Q   .��6]   Cancel6e   �� 5�      $ . $��2��66   $��5q�6F   $��30��6Q   ��6]   �� *Select+.��6x   Select6�   5�      5�      5�      5�      