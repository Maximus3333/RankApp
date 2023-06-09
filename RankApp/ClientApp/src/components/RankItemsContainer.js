import { useState } from 'react';
import RankItems from './RankItems';

const RankItemsContainer = ({dataType }) => {
    const albumLocalStorageKey = "albums";
    const movieLocalStorageKey = "movies";

    var localStorageKey = "";

    const [albumItems, setAlbumItems] = useState(JSON.parse(localStorage.getItem(albumLocalStorageKey)));
    const [movieItems, setMovieItems] = useState(JSON.parse(localStorage.getItem(movieLocalStorageKey)));



    var data = [];
    var setFunc = null;

    if (dataType === 'movie') {
        data = movieItems;
        setFunc = setMovieItems;
        localStorageKey = movieLocalStorageKey;

    }
    else if (dataType === 'album') {
        data = albumItems;
        setFunc = setAlbumItems;
        localStorageKey = albumLocalStorageKey;

    }

    return (
        <RankItems items={data} setItems={setFunc} dataType={dataType} localStorageKey={localStorageKey } />    
    )


}
export default RankItemsContainer;