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
import {useDispatch} from "react-redux";
import {AppDispatch} from "../redux/store"
import AuthService from "../redux/services/AuthService"
import {LoginModel,RegistrationModel} from '../models/RequestModel';
import sha256 from "sha256";
import {RegisterSuccess} from "../redux/actions/authActions";
import { clientActions } from "../redux/slices/clientslice";

interface RegProps {
	regOpen: boolean,
	regHandlerClose: () => void
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

function RegisterModal(props: RegProps){

  interface State {
    name: string,
    surname: string,
    email: string,
    mainpassword: string,
    passwordcheck: string
  }

  const [wrongPassShow,setWrongPassShow] = React.useState(false);
  const toggleWrongPassShow = () => setWrongPassShow(!wrongPassShow);
  const [successShow,setSuccessShow] = useState(false);
    const toggleSuccessShow = () => setSuccessShow(!successShow)
    const [noDataShow,setNoDataShow] = React.useState(false);
    const toggleNoDataShow = () => setNoDataShow(!noDataShow);
    const [open, setOpen] = React.useState(false);
    const handleOpen = () => setOpen(true);
    const navigate = useNavigate();
    const handleClose = () => props.regHandlerClose();
    const dispatch = useDispatch<AppDispatch>();
    const [show, setShow] = useState(false);
    const toggleShow = () => setShow(!show);
    const [nullShow, setNullShow] = useState(false);
    const toggleNullShow = () => setNullShow(!nullShow);
    const [values, setValues] = useState<State>({
      name: '',
      surname: '',
      email: '',
      mainpassword: '',
      passwordcheck: ''
  })
    
        const handleChange = (prop: keyof State) => (event: React.ChangeEvent<HTMLInputElement>) => {
            setValues({...values, [prop]: event.target.value.trim()});
        };
        const signUp = (event: any) => {
           if (values.mainpassword !== values.passwordcheck){setWrongPassShow(true); return;} 
           if (values.name===""||values.surname===""||values.email===""||values.mainpassword===""){setNoDataShow(true);return;}
          const data: RegistrationModel = {
            password: sha256(values.mainpassword),
            name: values.name,
            surname: values.surname,
            email: values.email
          };
          AuthService.register(data).then((res) => {
            dispatch(res)
            if (res.type === clientActions.registerSuccess.type){
              
              handleClose();
              setSuccessShow(true);
            }
          });
        };

    return(
        <>
        <Modal
  open={props.regOpen}
  onClose={handleClose}
  aria-labelledby="modal-modal-title"
  aria-describedby="modal-modal-description"
>
  <Box sx={style}>
  <Typography id="modal-modal-title" variant="h6" component="h2" >
            Регистрация
          </Typography>
          <TextField fullWidth label="Фамилия" onChange={handleChange("surname")} sx={{
            mt:2,
            mb:2
            }}>

          </TextField>
          <TextField fullWidth label="Имя" onChange={handleChange("name")} sx={{
            }}>

          </TextField>
          <TextField fullWidth type="email" label="Email" onChange={handleChange("email")} sx={{
            mt:2,
            mb:2
            }}>

          </TextField>
          <TextField fullWidth label="Пароль" onChange={handleChange("mainpassword")} type="password">

          </TextField>
          <TextField fullWidth label="Повторите пароль" onChange={handleChange("passwordcheck")} type="password" sx={{
            mt:2,
            mb:2
            }}>

            </TextField>
            <Button variant="contained" onClick={signUp} sx={{
            }}>
                Зарегистрироваться
            </Button>
          
  </Box>
</Modal>
<Snackbar open={wrongPassShow} autoHideDuration={5000} onClose={toggleWrongPassShow}>
        <Alert onClose={toggleWrongPassShow} severity="error" sx={{ width: '100%' }}>
          Пароли не совпадают!
        </Alert>
      </Snackbar>
      <Snackbar open={noDataShow} autoHideDuration={5000} onClose={toggleNoDataShow}>
        <Alert onClose={toggleNoDataShow} severity="error" sx={{ width: '100%' }}>
          Заполните все поля!
        </Alert>
      </Snackbar>
      <Snackbar open={successShow} autoHideDuration={5000} onClose={toggleSuccessShow}>
        <Alert onClose={toggleSuccessShow} severity="success" sx={{ width: '100%' }}>
          Регистрация прошла успешно.
        </Alert>
      </Snackbar>

  </>)
}



export default RegisterModal;