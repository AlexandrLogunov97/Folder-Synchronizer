   M S B A M L   `   `   `  ����@  <Work1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Work1.Views.CreateRepositoryWork1.Views.CreateRepository  5      < 8S, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null clr-namespace:S;assembly=SS 5      7    1Work1.Views.CreateRepository.SelectTypeConnection   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:Work1.Views.CreateRepository     5      (sclr-namespace:S;assembly=S     5        N� Title$  SelectTypeConnection��5
       N� 
Background
 ����� � 	Resources 5         Ftp �� 
TargetType �� Property �� Value �� Triggers �� Binding �� ElementName	 �� Path
 �� Value  FS��%�  &	        &	 �     �� 5   
   " ��6   �5      �� 5       � y�$ 	Collapsed=�6/   6C    5      �� 5       6   �� $	 FTP��$	 	IsChecked�
 True6Q   .a�5      ��  � y�$ Visible=�63   6E   5      5      �� 5   
   " ��6   �5      �� 5       � y�$ 	Collapsed=�6/   6C    5      �� 5       6   �� $ FS��$	 	IsChecked�
 True6P   .a�5      ��  � y�$ Visible=�63   6E   5      5      5       .��5!      � 
������� � ColumnDefinitions 5"   
   �� 5#      �� 5$      �� 5%       � RowDefinitions 5'   
   �� 5(      $
��auto �6   �� 5)      $
��auto �6   �� 5*      $
��auto �6   �� 5+      .+�5-   
   �� $��1��6   $��0��6    $��18(�6-   $��40��6;   $��Center=�6G   $��Center=�6i   .��6�   Type of connection6�   �� 5.   
   -   5         ��Name$
 Type��5.      $��1��6#   $��1��63   $��40��6@   .+�5/      	� -   5         	�Name$ FS��5/   +    d� 	IsChecked$
 TrueY�6   $��File system��67   6O   	� 50      -   5        $	 FTP��50      $	��FTP��6'   $��0,5,0,0q�65   6H   51      �� 52   
   $��1��6   $��2��6%   .+�53      �� -   5         ��Name$ FtpPanel��53      #��[  6(   ._�54      �� .+�55      �� $��16(�6   .��6*   Uri6/   �� 56      $��40��6   $��0,5,0,0q�6+   $��Center=�6<   $��16(�6^   ��6l   �� *FtpUri+6�   �� 57      $��16(�6   .��6*   	User name65   �� 58      $��40��6   $��0,5,0,0q�6+   $��Center=�6<   $��16(�6^   6n   �� 59      $��16(�6   .��6*   
Password64   �� 5:      $��40��6   $��0,5,0,0q�6+   $��Center=�6<   $��16(�6^   6n   �� 5;        S.Navigation   Attach . 
   Uri$$ Views/FtpViewer/ftpViewer.xaml��68   $��40��6�   $��0,5,0,0q�6�   $��18(�6�   ��6j   �� *CreateConnection+.��6�   	Connect6�   5<      5=      5>      �� 5?   
   -   5        $ FSPanel��5?      $��1��6&   $��2��66   .+�5@      �� #��[ 6   ._�5A      �� .+�5B      ��  . $2 ,Views/FileSystemViewer/FileSystemViewer.xaml��68   $��40��6�   $��0,5,0,0q�6�   $��18(�6�   ��6x   �� *SelectDirectory+.��6�   Select6�   5C      5D      5E      5F      5G      