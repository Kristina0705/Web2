import React, {useState} from "react";
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import Button from '@mui/material/Button';
import { TextField, Typography } from '@mui/material';
import {useNavigate} from 'react-router-dom';
import "bootstrap/dist/css/bootstrap.min.css";
import "../assets/css/index.css";
import Snackbar from '@mui/material/Snackbar';
import Alert from '@mui/material/Alert'; 
import RegisterModal from './RegisterModal';
import {useDispatch} from "react-redux";
import {AppDispatch} from "../redux/store"
import AuthService from "../redux/services/AuthService"
import {LoginModel} from '../models/RequestModel';
import sha256 from "sha256";
import { clientActions } from "../redux/slices/clientslice";

interface AuthProps {
	open: boolean,
	handlerClose: () => void
}

const style = {
    position: 'absolute' as 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4,
    display:"flex",
    flexDirection:"column",
    alignItems:"center"
  };

function AuthModal(props: AuthProps){

    interface State {
        email: string,
        password: string
    }
    const [open, setOpen] = React.useState(false);
    const [regOpen,setRegOpen] = React.useState(false);
    const handleRegOpen = () => { handleClose(); setRegOpen(true)}
    const handleOpen = () => setOpen(true);
    const navigate = useNavigate();
    const handleClose = () => props.handlerClose();
    const dispatch = useDispatch<AppDispatch>();
    const [successShow,setSuccessShow] = useState(false);
    const toggleSuccessShow = () => setSuccessShow(!successShow)
    const [show, setShow] = useState(false);
    const toggleShow = () => setShow(!show);
    const [nullShow, setNullShow] = useState(false);
    const toggleNullShow = () => setNullShow(!nullShow);
        const [values, setValues] = useState<State>({
            email: '',
            password: ''
        });
    
        const handleChange = (prop: keyof State) => (event: React.ChangeEvent<HTMLInputElement>) => {
            setValues({...values, [prop]: event.target.value.trim()});
        };
        const logIn = () => {
            const data: LoginModel = {
                email: values.email,
                password: sha256(values.password)
            };
            AuthService.login(data).then((res) => {
                dispatch(res)
                if (res.type === clientActions.loginSuccess.type) {

                    handleClose();
                    setSuccessShow(true);

                }
                else {setShow(true)}
            })

        }

    return(
        <>
        <Modal
  open={props.open}
  onClose={handleClose}
  aria-labelledby="modal-modal-title"
  aria-describedby="modal-modal-description"
>
  <Box sx={style}>
  <Typography id="modal-modal-title" variant="h6" component="h2" >
            Авторизация
          </Typography>
          <TextField fullWidth type="email" label="Email" onChange={handleChange("email")} sx={{
            mt:2,
            mb:2
            }}>

          </TextField>
          <TextField fullWidth label="Пароль" type="password" onChange={handleChange("password")}>

          </TextField>
          <Box sx={{
            mt:2,
            mx:"auto",
            display:"flex",
            justifyContent:"space-between"
          }}>
            <Button variant="contained" onClick={logIn} sx={{
                mr:8
            }}>
                Войти
            </Button>
            <Button variant="text" sx={{
                ml:8
            }} onClick={handleRegOpen}>
                Регистрация
            </Button>
          </Box>
          
  </Box>
</Modal>
<RegisterModal regOpen={regOpen} regHandlerClose={() => setRegOpen(false)}/>
<Snackbar open={show} autoHideDuration={5000} onClose={toggleShow}>
        <Alert onClose={toggleShow} severity="error" sx={{ width: '100%' }}>
          Неверный логин или пароль!
        </Alert>
      </Snackbar>
      <Snackbar open={successShow} autoHideDuration={5000} onClose={toggleSuccessShow}>
        <Alert onClose={toggleSuccessShow} severity="success" sx={{ width: '100%' }}>
          Авторизация прошла успешно.
        </Alert>
      </Snackbar>
  </>)
}



export default AuthModal;