namespace Dotnet9.Application.Config.Dtos;

public class CustomControl
{
    /// <summary>
    /// 控件类型
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// 控件配置项
    /// </summary>
    public ControlOptions Options { get; set; }
}

public class ControlOptions
{
    /// <summary>
    /// 控件唯一名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 控件中文名
    /// </summary>
    public string Label { get; set; }

    public string Type { get; set; }

    /// <summary>
    /// 默认值
    /// </summary>
    public object DefaultValue { get; set; }

    /// <summary>
    /// 控件占位符
    /// </summary>
    public string Placeholder { get; set; }

    /// <summary>
    /// 是否必填
    /// </summary>
    public bool Required { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    public string LabelToolTip { get; set; }

    /// <summary>
    /// 精度
    /// </summary>
    public int Precision { get; set; }

    /// <summary>
    /// 递增值
    /// </summary>
    public int Step { get; set; }

    /// <summary>
    /// 允许半选
    /// </summary>
    public bool AllowHalf { get; set; }

    public OptionItem[] OptionItems { get; set; }

    /// <summary>
    /// 附件最大上传数量
    /// </summary>
    public int Limit { get; set; }

    /// <summary>
    /// 是否启用多选
    /// </summary>
    public bool Multiple { get; set; }

    public class OptionItem
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public OptionItem[] Children { get; set; }
    }
}