   M S B A M L   `   `   `  ����\  XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source   ComboBoxToggleButton �� 
TargetType � ColumnDefinitions ��Name  HighLightBackgroundCRM  FocusBorderCRM �� RadiusX �� RadiusY  DefaultForegroundCRM  DefaultBorderCRM F�Name  
GlyphBrush �� Triggers	 P� Property
 P� Value �� 
TargetName �� Property �� Value  PressedForegroundCRM  PressedBorderCRM  DisabledForegroundBrush 	 DisabledBorderCRM 
 ComboBoxTextBox ��Name  SimpleScrollViewer �� 
TargetType �� BasedOn � RowDefinitions �� CanContentScroll ��Name, �� %ComputedHorizontalScrollBarVisibility  SimpleScrollBar �� Orientation �� ViewportWidth �� ViewportSize � Minimum �� ScrollableWidth � Maximum4   .System.Windows.Automation.AutomationProperties    AutomationId � Value �� Path �� Mode �� RelativeSource*  �� #ComputedVerticalScrollBarVisibility! �� ViewportHeight" �� ScrollableHeight  LabelNewStyleCRM# ��Name$ �� CornerRadius & "ButtonBarBackgroundDefaultBrushCRM% �Name& ��Name * &ToggleButtonEditableControlTemplateCRM  DefaultLightBackgroundCRM  ComboBoxBackgroundCRM  TextboxBorderCRM  DisabledBorderBrush  ComboBoxStyleCRM' d�Name( �� 	ClickMode) d� 	IsChecked* �� SelectionBoxItem+ �� SelectionBoxItemTemplate, J� IsHitTestVisible- ��Name  TextBoxStyleCRM. �Name/ �� IsDropDownOpen0 � AllowsTransparency1 �� MaxDropDownHeight2 � ActualWidth3 J� SnapsToDevicePixels " ScrollViewerControlTemplateCRM4 K� IsItemsHost-   'System.Windows.Input.KeyboardNavigation5   DirectionalNavigation6 P� 
SourceName�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5        5      �� 5      $ Brushes.xaml?�6   5      %I  &	        &	
 <    &	 �    &	 h
    &	 W    &	 �    �� 5
      " d�60   .R�5      �  5      �� 5      �� 5      $��20 �6   .+�5      �� $ 	Highlight��6   $��2��6"   #���  66   $y�Hidden=�6f   �� 5      $ FocusBorder��6   #���  6$   $��1��6N   $��1,1,1,1q�6b   $��2��6s   $ 3��6�   $ 3��6�   $y�Hidden=�6�   �� 5      $ 	Rectangle��6A   $��2��6   $��Stretch=�6#   $��Stretch=�6T   $
��Auto��6p   $
��Auto��6}   $ 3��6�   $ 3��6�   #���  6�   #���  6�   F� 5      $ Arrow��67   $��1��6
   $��Center=�6   $��Center=�6F   #���  6a   "����        � 	=  	= � z     	6�   5       5      P� 5      	 �@J�	IsFocused
 . . �5      �� $ FocusBorder��6#    � y�$ Visible=�6<   �� 5      $ 	Highlight��6     �@J�Opacity$ 0Q�67   �� 5      $ FocusBorder��6(    � ��$ 1��6A   5      P� 5      	 �@J�IsMouseOver
 . . �5      �� $ 	Highlight��6#    � y�$ Visible=�6:   �� 5      $ 	Highlight��6     �@��RadiusX$ 3��67   �� 5      $ 	Highlight��6     �@��RadiusY$ 3��67   5      P� 5       	 �@d�	IsChecked$

 trueY�6"   . �5!      �� $ 	Rectangle��6   # �  64    � ���� 5"      $ 	Rectangle��6   # �  66    � ���� 5#      $ 	Highlight��6#    � y�$ Visible=�6:   5$      P� 5%      	 � {�
 .  . �5&      �� # �  6#    � ���� 5'      # �  6   $ Arrow��6O    � ���� 5(      $ 	Rectangle��6   # � 	 66    � ���� 5)      $ 	Rectangle��6    64   Z�  � ��5*      5,      �� 5.      " ��6+   .R�50      �� $ PART_ContentHost��6   
������ |�.  $��Hidden=�6V   $��Hidden=�6}   51      �� 56      " ��6$    6G   Z� .�57      ��  � �� 58      �� 59      " ��6   .R�5:      � #��z��6    5;      �� 5<   	   $��* �6   �� 5=   	   $
��Auto �6    5?      �� 5@   	   $��* �6   �� 5A   	   $
��Auto �6   .+�5C      �� $��0��6   $��0��6/   #��z��6<   #��z��6_   #��z��6�   # z��6�   �� 5F      $ PART_HorizontalScrollBar��6t   #y�z 6   $��0��6W   $��1��6g   #���  6�   $ 
Horizontal=�6�   # z 6?  $ 0Q�6n  # z 6z  $ HorizontalScrollBar��6�   6�   �� $ HorizontalOffset�$ OneWay=� �� *TemplatedParent+6�  �� 5G      $ PART_VerticalScrollBar��6r   #y�z  6   $��1��6U   $��0��6e   #���  6�   $ Vertical=�6�   # z! 67  $ 0Q�6g  # z" 6s  $ VerticalScrollBar��6�   6�   �� $ VerticalOffset�$ OneWay=� �� *TemplatedParent+6�  5H      5I      5K      5L      �� 5N      " ��6%   .�5O   
   ��  �@J�SnapsToDevicePixels . 60   �� 5P   
    � �� 5Q      �� 5R      " ��6"   .R�5S      � .+�5T      �� $# 	HighLight��6!   $$ 0,7,0,0��64   #���  6K   � 5U      $	% Top��6   $y�Visible=�6,    5V      �� 5W   "   $��12 �60   �� 5X   "   $��* �60    5Z      �� 5[   "   $��* �63   �� 5\   "   $��12 �63   .+�5^      �� $��1��6$   $\��V/MSExchangeClient.Infrastructure;component/Styles/Images/Glasspanel10-clear_10-05+.png��61   $��1��6�   $��1��6�   $
��Fill=�6�   �� 5_      $��1��6$   $_��Y/MSExchangeClient.Infrastructure;component/Styles/Images/GlasspanelpanelButtons_01-03.png��64   $
��Fill=�6�   �� 5`      $��0��6$   $c��]/MSExchangeClient.Infrastructure;component/Styles/Images/GlasspanelpanelButtons_sliced_03.png��64   $
��Fill=�6�   5a      �� 5c      $& ContentSite��6W   $
��Left=�6+   $��3,3,7,3q�6F   $��Center=�6l   5d      5e      5g      5h      �� 5j      " d�6E   .R�5k   
   �  5l      �� 5m      �� 5n      $��20 �6#   .+�5p      �� $ 	Highlight��6   $��2��6+   $ 4��6?   $ 4��6K   $y�Visible=�6W   $��Stretch=�6l   #���  6�   $��2��6�   $��0,0,0,0q�6�   �� 5q      $# border2��6   $$ 0,3,3,0��6<   $��1��6S   ��6&   Z� 6S   �� 5r      $# border1��6�   #���  6   #���  6J   $��1,1,1,1q�6{   $$ 0,3,3,0��6�   $��1��6�   �� 5s      $# border��6�   #���  6   #���  6J   $��1,1,0,1q�6{   $$ 3,0,0,3��6�   6�   F� 5t      $ Arrow��6@   $��1��6   $��Center=�6#   $��Center=�6O   #���  6j   "����        � 	=  	= � z     	6�   5u       5v   
   P� 5w      	 �@J�IsMouseOver
 . . �5x      �� $ border1��60   # �  6E    � ���� 5y      $ border1��6/   # �  6D    � ���� 5z      $ border2��6/    6D   Z�  � ��5{      P� 5|      	 �@d�	IsChecked$

 trueY�6+   . �5}      �� $ border1��6/   # �  6D    � ���� 5~      $ border1��60   # �  6E    � ���� 5      $ border2��6/   # �  6D    � ��5�      P� 5�      	 � {�
 .  . �5�      �� # �  6/    � ���� 5�      # �  6)   $ Arrow��6[    � ���� 5�      $ border��60   # �  6D    � ���� 5�      $ border1��60   # �  6E    � ���� 5�      $ border1��6/    6D   Z�  � ���� 5�      $ border��6/    6C   Z�  � ���� 5�      $ border2��6/    6D   Z�  � ���� 5�      $ 	Highlight��6)    6@   Z�  � ��5�      5�      �� 5�      " ��6%   .�5�      ��  �@J�SnapsToDevicePixels . 6*   �� 5�       � �� 5�      �� 5�      " ��6   .R�5�      � .+�5�      d� $' ToggleButton��6W   $��2��6   #���   6%   |�.  $( Press=�6�   ) 6   �� $ IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+6�   �� 5�      $& ContentSite��6F   $
��Left=�6   $��3,3,23,3q�64   $��Center=�6[   #��z* 6v   #��z+ 6�   #��z��6�   , .  6!  �� 5�      $- PART_EditableTextBox��6�   $��68��6   $y�Hidden=�6   #��� 
 6/   $
��Left=�6\   $��3,3,3,3q�6w   #���  6�   $��Center=�6�   |�. 
������ 6�   � 5�      $. Popup��6K   #��z/ 6   $��Bottom=�68   |�.  0 .  $��Slide=�6�   .��5�   	   � $% DropDown��6g   #��z1 6   #��z2 6>   3 . .+�5�   
   �� $# DropDownBorder��6   ���	#FFFFFFFF���	#FF7E7E7E$��1q�6X   �� 5�   
   $��4,6,4,6q�6   #���  6(   3 . $
��Auto=�6p   $
��Auto=�6�   ��. #���  6�   .��5�      �� 4 . $5 	Contained=�6)   5�      5�   
   5�   	   5�       5�      P� 5�      	 �@��HasItems
 .  . �5�   	   �� $ DropDownBorder��60    � ��$ 95��6%   60   5�   	   P� 5�      	 � {�
 .  . �5�   	   �� # �  6&    � ��5�   	   P� 5�      	 �@��
IsGrouping
 . . �5�   	   ��  � �� .  69   5�   	   P� 5�      $6 Popup��6.   	 �@�AllowsTransparency
 . . �5�   	   �� $ DropDownBorder��62    �@��CornerRadius$ 4��6(   62   �� 5�   	   $ DropDownBorder��62    � ��$ 0,2,0,0q�6"   62   5�   	   P� 5�      	 �@��
IsEditable
 . . �5�   	   ��  � �� .  6%   �� 5�   	   $ PART_EditableTextBox��66    � y�$ Visible=�6&   66   �� 5�   	   $ ContentSite��65    � y�$ Hidden=�6&   65   �� 5�   	   $ ToggleButton��6$   # �  6>    � ��5�   	   5�      5�      5�      5�      