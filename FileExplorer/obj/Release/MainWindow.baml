   M S B A M L   `   `   `  ���� G  CFileExplorer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:FileExplorerFileExplorer  A%clr-namespace:FileExplorer.PropertiesFileExplorer.Properties      FileExplorer.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:FileExplorer     6resx%clr-namespace:FileExplorer.Properties     V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null:md1http://materialdesigninxaml.net/winfx/xaml/themes    � AllowsTransparency  .  � Icon$ icon.ico��
������  � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=�
�������$
��1040��$	��620�� � 	Resources    item �� Property �� Value  HeaderStyle  	ListStyle  DialogInfoStyle��%�  &	        &	 $     &	 �     &	 5    �� .���  � ��$ 20 0 0 0q��� .���  � �� �	#FF636363��  � ��$ 12(���  � ��$ Center=���  �@��Padding$ 10 0 0 0q���  � ��$ Roboto Normal)��� .���  � �� �	#FF636363��  � ��$ 14(���  � ��$ Center=���  � ��$ Roboto Light)��� .���  � ��
 �   ���  � ��$ 14(���  � ��$ 0 0 0 10q�.��� $��20q����	#FFF7F7F7 J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 10Q�	   	Direction$		 -90Q�
   RenderingBias$
 Quality=�   Opacity$	 0.4Q�   ShadowDepth$ 2Q� � RowDefinitions �� $��96 ��� $��* �.+��� -   $��0�� �� Orientation$ Vertical=�.+�� $��32�����	#FF24609E.+��� '   !FileExplorer.Properties.Resources   	app_title#��Z $��Roboto Medium)�$��Center=�$��Center=�$��16(��� $ 
Horizontal=�$��Right=�.+��� -   $��	10 0 10 0q�
���������Z� ��Z� .��'  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]   Kind$ ColorHelper=��� -   $��	10 0 10 0q�
���������Z� ��Z� .��  $��20��$��20��$ Close=�� $��64��$��Center=����#3F7EC1.+��� $��10 0 0 0q�$ 
Horizontal=�$��Center=�$
��Left=�.+��� -    ��Name$ button_back��
������� � ToolTip$
 Back�� / +MaterialDesignFloatingActionMiniLightButton#��[ ��Z� ��Z� .��  $��30��$��30��$ 	ArrowLeft=��� -    ��Name$ text_current_dir��$��Computer��$��20 0 0 0q�$��Center=�$��Roboto Medium)�$��20(��� $ 
Horizontal=�$��Center=�$��Right=�.+��� -   $ button_copy��$y�	Collapsed=�$��0 0 10 0q�
�������#��[    
hint_paste# Z ��Z� ��Z� .��  $��30��$��30��$ ContentPaste=��� -   $ button_new_folder��$��0 0 10 0q�
�������#��[    title_edit_new_folder# Z ��Z� ��Z� .��  $��30��$��30��$
 Plus=�� $��2�� � ColumnDefinitions �� $	��200 ��� $	��800 �.+��� 
�������$��0��$ Vertical=�.+��� $��Places�� �� Padding$ 12q����	#FFB8B8B8$��Roboto Normal)�$��14(��� -    ��Name$ list_navigation�� �� SelectionMode$ Single=����� .R��� $ 
Horizontal=�$��10 0 0 0q�.+�  ���	#FF747474$��25��$��25��$��Center=� ��  �� Path$
 icon��� $��Roboto Normal)�$��Center=�$��
20 10 0 10q����� $
 name�� $��1�� �� $	��540 ��� $	��120 ��� $	��140 � �� $��40 ��� $��* �.+��� $��Bottom=�$��3���� $
��Name��$��0��#��[ �� $
��Type��$��1��#��[ $��Center=��� $��Last modified��$��2��#��[ $��Center=��� -	   $ 
list_items��$��Hidden=�$ Single=�$��1��$��3������ .R��  �� $	��540 ��� $	��120 ��� $	��140 �.+��� $��0��$ 
Horizontal=� � ContextMenu �� $	��150��.@�m� -
   $
��Copy���� m� -   $��Delete��m� -   $��Rename���� m� -   $��
Properties��.+�  ���#3F7EC1$��20��$��20��$��Center=� �� $
 icon��� #��[ $��20 0 0 0q�$��Roboto Normal)�$��0������ $
 name��� #��[ $��1��$��Center=����� $
 type��� #��[ $��2��$��Center=����� $ modifiedTime�)  #MaterialDesignThemes.Wpf.DialogHost  -     Name$ dialog_edit��$��3��   DialogContent �� $	��400��$��20q�$ Vertical=�.+��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�.+�  -     Name$ 	icon_edit��$��22��$��22�����#3F7EC1�� -   $ text_edit_title��   title_edit_rename#��Z $��Roboto Normal)�$��18(�$��20 0 0 0q����#3F7EC1�� -     ��Name$  
input_name��!   hint_new_folder)  #MaterialDesignThemes.Wpf.HintAssist"   Hint#" Z! #   FloatingScale$# 1Q�$   
IsFloating$ . $��0 0 0 20q�.���� $ 
Horizontal=�$��Right=�.+��� %   CloseDialogCommand#��Z%   MaterialDesignFlatButton#���  $��12(�$��	10 5 10 5q�$��Roboto Light)�&   button_cancel#��Z& �� -   $ button_edit_accept��#���  $��12(�$��	10 5 10 5q�$��Roboto Light)�'   button_create#��Z'   -   $ dialog_delete��$��3�� �� $	��400��$��20q�$ Vertical=�.+��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�.+�  $ Delete=�$��22��$��22�����#3F7EC1�� -   $ text_delete_file_title��$��Delete��$��Roboto Normal)�$��18(�$��20 0 0 0q����#3F7EC1�� -   $ text_delete_file_descr��(   descr_delete_file#��Z( 
���   �$��16(�$��0 0 0 10q��� -   $ text_delete_file_info��$��MyFolder��#��[ �� $ 
Horizontal=�$��Right=�.+��� #��Z% #���  $��14(�$��	10 5 10 5q�$��Roboto Light)�#��Z& �� -   #���  $��14(�
���  ��$��	10 5 10 5q�$��Roboto Light)�)   button_delete#��Z)   -   $ dialog_properties��$��3�� �� $	��500��$��20q�$ Vertical=�.+��� $ 
Horizontal=�$��Center=�$��0 0 0 20q�.+�  $ 	Animation=�$��22��$��22�����#3F7EC1�� *   title_properties#��Z* $��Roboto Normal)�$��18(�$��20 0 0 0q����#3F7EC1�� -   $ text_prop_top_info��$��Name: folder��#��[ �� $��0 0 0 10q��� -   $ text_prop_mid_info��$��Location: D��#��[ �� $��0 0 0 10q��� -   $ text_prop_low_info��$��Creation time: today��#��[ �� #��Z% $��Right=�#���  $��12(�$��	10 5 10 5q�$��Roboto Light)�+   	button_ok#��Z+ 