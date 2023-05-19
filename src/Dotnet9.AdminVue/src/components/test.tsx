import { ElButton, ElInput, ElSwitch } from "element-plus";
import {
  defineComponent,
  render,
  PropType,
  ref,
  onMounted,
  reactive,
} from "vue";
import { BaseOption, SelectOption } from "./SimpleFormModel";

const Test = defineComponent({
  props: {
    items: {
      type: Object as PropType<BaseOption[]>,
      required: true,
    },
  },
  setup(props) {
    const testHandler = () => {
      console.log("点击一下");
    };

    onMounted(() => {
      console.log("组件启动");
    });

    const point = reactive({
      x: 0,
      y: 0,
    });

    const name = ref("123");

    return () => (
      <>
        {props.items?.map((item) => {
          if (item.control == "input") {
            return <h1>input</h1>;
          }
          if (item.control == "select") {
            var control = item as SelectOption;
            return (
              <div>
                {control.items?.map((config) => {
                  return (
                    <p>
                      配置:{config.label} - {config.value}
                    </p>
                  );
                })}
              </div>
            );
          }
          return null;
        })}
        <ElInput v-model={name.value}></ElInput>
        <ElButton onClick={testHandler}>测试-click</ElButton>
      </>
    );
  },
});

export default Test;
