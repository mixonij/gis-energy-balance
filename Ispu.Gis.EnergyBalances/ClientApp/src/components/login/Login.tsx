import {useCallback, useContext} from "react";
import {useNavigate} from "react-router-dom";
import {AuthenticationContext} from "../../contexts/authentication-context";
import {AuthenticateRequest} from "../../models/authentication";
import {Md5} from "ts-md5";
import {useForm, Controller} from 'react-hook-form';
import {Button} from "primereact/button";

import "./Login.css";
import classNames from "classnames";
import {InputText} from "primereact/inputtext";
import {Password} from "primereact/password";

type LoginProps = {
    redirectPath?: string | null
}

export const Login = ({redirectPath}: LoginProps) => {
    const authContext = useContext(AuthenticationContext);

    let navigate = useNavigate();
    const routeChange = useCallback(() => {
        let path = redirectPath !== undefined && redirectPath ? redirectPath : "/";
        navigate(path);
    }, [navigate, redirectPath]);

    const onLogin = useCallback(async (login: string, password: string) => {
        const payload = new AuthenticateRequest();
        payload.initFromValues(login, Md5.hashStr(password));

        await authContext
            ?.authenticateUser(payload)
            .then(routeChange)
            .catch(console.error);
    }, [authContext, routeChange]);

    const defaultValues = {
        email: '',
        password: ''
    }

    const {control, formState: {errors}, handleSubmit} = useForm({defaultValues});


    const getFormErrorMessage = (name: any) => {
        return (errors as any)[name] && <small className="p-error">{(errors as any)[name].message}</small>
    };

    const onSubmit = async (data: any) => {
        await onLogin(data.email, data.password).catch(console.error);
    };

    return (
        <div className="login-form card">
            <div className="flex justify-content-center">
                <div className="cont form">
                    <h5 className="text-center">Вход в учетную запись</h5>
                    <form onSubmit={handleSubmit(onSubmit)} className="p-fluid">
                        <div className="field">
                            <label htmlFor="email"
                                   className={classNames({'p-error': !!errors.email})}>Адрес электронной почты
                                пользователя*</label>
                            <span className="p-float-label p-input-icon-right">
                                <i className="pi pi-envelope"/>
                                <Controller name="email" control={control}
                                            rules={{
                                                required: 'Необходимо ввести адрес электронной почты пользователя',
                                                pattern: {
                                                    value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i,
                                                    message: 'Некорректный адрес электронной почты. Например, example@ispu.ru'
                                                }
                                            }}
                                            render={({field, fieldState}) => (
                                                <InputText id={field.name} {...field}
                                                           className={classNames({'p-invalid': fieldState.error})}/>
                                            )}/>
                               
                            </span>
                            {getFormErrorMessage('email')}
                        </div>
                        <div className="field">
                            <label htmlFor="password"
                                   className={classNames({'p-error': errors.password})}>Пароль пользователя*</label>
                            <span className="p-float-label">
                                <Controller name="password" control={control}
                                            rules={{required: 'Необходимо ввести пароль'}}
                                            render={({field, fieldState}) => (
                                                <Password id={field.name} {...field} toggleMask feedback={false}
                                                          className={classNames({'p-invalid': fieldState.error})}/>
                                            )}/>
                            </span>
                            {getFormErrorMessage('password')}
                        </div>
                        <Button type="submit" icon="pi pi-sign-in" label="Выполнить вход" className="mt-2"/>
                    </form>
                </div>
            </div>
        </div>
    );
};
