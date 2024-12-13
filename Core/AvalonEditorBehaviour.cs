using System.Windows;
using System.Windows.Interactivity;
using ICSharpCode.AvalonEdit;

namespace weirditor.Core;

public class AvalonEditorBehaviour: Behavior<TextEditor>
{
    public static readonly DependencyProperty EditorTextProperty =
        DependencyProperty.Register(nameof(EditorText), typeof(string), typeof(AvalonEditorBehaviour), 
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PropertyChangedCallback));

    public string EditorText
    {
        get { return (string)GetValue(EditorTextProperty); }
        set { SetValue(EditorTextProperty, value); }
    }

    protected override void OnAttached()
    {
        base.OnAttached(); 
        if (AssociatedObject != null) 
        { 
            AssociatedObject.TextChanged += AssociatedObjectOnTextChanged;
            AssociatedObject.Text = EditorText; 
        }
    }
    protected override void OnDetaching()
    {
        base.OnDetaching();
        if (AssociatedObject != null)
            AssociatedObject.TextChanged -= AssociatedObjectOnTextChanged;
    }

    private void AssociatedObjectOnTextChanged(object sender, EventArgs eventArgs)
    {
        var textEditor = sender as TextEditor;
        if (textEditor != null)
        {
            if (textEditor.Document != null)
                EditorText = textEditor.Document.Text;
        }
    }

    private static void PropertyChangedCallback(
        DependencyObject dependencyObject,
        DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
    {
        var behavior = dependencyObject as AvalonEditorBehaviour;
        if (behavior.AssociatedObject!= null)
        {
            var editor = behavior.AssociatedObject;
            if (editor.Document != null)
            {
                var caretOffset = editor.CaretOffset;
                editor.Document.Text = dependencyPropertyChangedEventArgs.NewValue.ToString();
                editor.CaretOffset = editor.Document.TextLength < caretOffset? editor.Document.TextLength : caretOffset;
            }
        }
    }
}