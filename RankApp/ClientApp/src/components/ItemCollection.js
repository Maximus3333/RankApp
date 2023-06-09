import Item from "./Item";
import Base64Image from "./ConvertBase64Image";

const ItemCollection = ({ items, drag }) => {
    console.log(items, "supp bro");
  return (
    <div className="items-not-ranked">
      {items.map((item) =>
        item.ranking === 0 ? (
          <Item
            key={`item-${item?.id}`}
            item={item}
            drag={drag}
            itemImgUrl={
                item.imageBase64 ?
              Base64Image({base64String: item.imageBase64}) 
              : item.imageUrl
                
            }
          />
        // <Base64Image
        //         base64String={item.imageBase64}
        //         alt={item.title}
        //       />
        ) : null
      )}
    </div>
  );
};
export default ItemCollection;
