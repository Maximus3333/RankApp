const Item = ({item, drag, itemImgUrl }) => {
    return (
        <div className="unranked-cell">
            <img id={`item-${item.id}`} src={itemImgUrl}
                style={{ cursor: "pointer" }} draggable="true" onDragStart={drag}
                alt="Supppp"
            />
        </div>     
    )
}
export default Item;