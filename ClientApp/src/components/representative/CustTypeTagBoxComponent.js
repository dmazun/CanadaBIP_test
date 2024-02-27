import React, { useCallback } from "react";
import TagBox from "devextreme-react/tag-box";

const nameLabel = { "aria-label": "cust_Type" };
const CustTypeTagBoxComponent = (props) => {

  const onValueChanged = useCallback((e) => {
    props.data.setValue(e.value.toString());
  }, [props]);

  const onSelectionChanged = useCallback(() => {
    props.data.component.updateDimensions();
  }, [props]);

  return (
    <TagBox
      dataSource={props.data.column.lookup.dataSource}
      defaultValue={props.data.value?.split(",").map((i) => i.trim())}
      valueExpr="cust_Type"
      displayExpr="cust_Type"
      showSelectionControls={true}
      maxDisplayedTags={3}
      inputAttr={nameLabel}
      showMultiTagOnly={false}
      applyValueMode="useButtons"
      searchEnabled={true}
      onValueChanged={onValueChanged}
      onSelectionChanged={onSelectionChanged}
    />
  );
};
export default CustTypeTagBoxComponent;
