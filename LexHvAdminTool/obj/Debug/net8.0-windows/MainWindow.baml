   M S B A M L   `   `   `  ����I  ELexHvAdminTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:LexHvAdminToolLexHvAdminTool  5   	   Q-clr-namespace:LexHvAdminTool.Helper.ConverterLexHvAdminTool.Helper.Converter  64       LexHvAdminTool.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:LexHvAdminTool     5   	   @local1-clr-namespace:LexHvAdminTool.Helper.Converter     64     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � 	Resources 5	      <   6LexHvAdminTool.Helper.Converter.NullToBooleanConverter   NullToBooleanConverter��%   &	          5
   
   6(   5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ 
FaqListBox��5       J� 	AllowDrop$
 True��5       �� View 5      �� 5      .P�5      �� $��FaqId��6%    �� Width$	 100��6[    �� DisplayMemberBinding 64   �� *FaqId+6[   �� 5      $��Title��6%   $	 400��6[    64   �� *Title+6[   5      5      �� 5    
    �� Orientation$ 
Horizontal=�6   $��1��6.   $��Center=�6;   $��10q�6X   .+�5!      �� -   5        $��New File��5!      $��5q�6D   �� 5"      -   5        $	��Add��5"      $��5q�6;   �� 5#      -   5        $
��Edit��5#      $��5q�6=   {�6H   �� 	 �� ElementName$	 
FaqListBox��
 �� Path$
 SelectedItem� �� 	Converter# [  �� 5$      -   5        $��Delete��5$      $��5q�6A   {�6L   �� $	 
FaqListBox��$
 SelectedItem�# [  �� 5%      -   5        $��Import��5%      $��5q�6A   �� 5&      -   5        $��Export��5&      $��5q�6A   �� 5'      -   5        $
��Save��5'      $��5q�6=   {�6H   �� $	 
FaqListBox��$
 HasItems�5(      �� 5*   
   $ 
Horizontal=�6   $��2��6.   $��Center=�6;   $��10q�6X   .+�5+      �� $��Last Modified:��6   $��5q�6.   �� 5,      -	   5         ��Name$ LastModifiedTextBlock��5,      $��5q�67   �� 5-      $��Modified By:��6   $��5q�6,   �� 5.      -
   5        $ ModifiedByTextBlock��5.      $��5q�65   5/      50      51      