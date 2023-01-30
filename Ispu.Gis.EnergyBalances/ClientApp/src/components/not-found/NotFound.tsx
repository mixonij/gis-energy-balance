import {Button} from "primereact/button";
import {useNavigate} from "react-router-dom";

export const NotFound = () => {
    let navigate = useNavigate();
    const routeChange = () => {
        let path = "/";
        navigate(path);
    }

    return (
        <div className="card">
            <div className="flex justify-content-center">
                <h1>Ошибка 404</h1>
            </div>
            <div className="flex justify-content-center">
                <h3 className="text-center">Запрашиваемая страница не найдена</h3>
            </div>
            <div className="flex justify-content-center mt-2">
                <Button label="Вернуться на главную" onClick={routeChange}/>
            </div>
        </div>
    );
}