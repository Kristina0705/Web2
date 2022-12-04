import React from 'react';
import FormControl from '@mui/material/FormControl';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import Select, { SelectChangeEvent } from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';
import InputLabel from '@mui/material/InputLabel';
import Button from '@mui/material/Button';
import AuthModal from './AuthModal';
import "bootstrap/dist/css/bootstrap.min.css";
import {useNavigate} from 'react-router-dom';
import "../assets/css/index.css";
import { useSelector} from "react-redux";
import { RootState} from "../redux/store";
import { Typography } from '@mui/material';
import Snackbar from '@mui/material/Snackbar';
import Alert from '@mui/material/Alert'; 
import {PostAppeal} from "../models/AppealModel"
import { TypeAppeal } from '../models/TypeAppealModel';
import AppealService from '../redux/services/AppealService';
import TypeAppealService from '../redux/services/TypeAppealService';


function PsixHome() {

  const handleChange = (event: SelectChangeEvent) => {
    setTmpAppeal({...tmpAppeal,typeappeal:event.target.value});
  };
   const handleOpen = () => {
    setOpen(true);
   }
   const [noDataShow,setNoDataShow] = React.useState(false);
   const noDataHandleShow = () => setNoDataShow(true)
   const noDataHandleClose = () => setNoDataShow(false)
   const [show,setShow] = React.useState(false);
   const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
   const [types,setTypes] = React.useState<TypeAppeal[]>([]);
   const user = useSelector((state: RootState) => state);
  const [open, setOpen] = React.useState(false);
  const navigate = useNavigate();
  const [key,setKey] = React.useState<boolean>(false);
  const [tmpAppeal,setTmpAppeal] = React.useState<PostAppeal>({
    typeappeal:"",
    text:""
  })

  const textChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setTmpAppeal({...tmpAppeal,text:event.target.value.trim()});
};

  React.useEffect(() => {
    if (key) return;
    TypeAppealService.getTypesAppeal().then((res)=>{
      if (res !== undefined){
        setTypes(res)
      }
    })
    setKey(true)
  }, [types,key])

  return (
    <>
    <Box sx={{
      backgroundColor:"#FFF",
      display:"flex",
      flexDirection:"column",
      alignItems:"center",
      width:"100%",
      margin:"15px auto",
    }}>
      <Typography variant="h4"  >
        Здравствуйте!
      </Typography>
      <Typography variant="h6" sx={{
        mt:1,
        mb:2
      }} >
        Здесь вы можете обратиться к нам за помощью. Наши специалисты обязательно постараются вам помочь.
      </Typography>
    <FormControl style={{
      width:"30%",

    }}>
  <InputLabel id="demo-simple-select-label">Тип обращения</InputLabel>
  <Select
    labelId="demo-simple-select-label"
    id="demo-simple-select"
    value={tmpAppeal.typeappeal}
    label="Age"
    onChange={handleChange}
  >
    {types.map((type)=>(
      <MenuItem value={type.TypeName}>{type.TypeName}</MenuItem>
    ))}
  </Select>
  <TextField multiline label="Текст обращения" onChange={textChange} fullWidth minRows={7} size="medium" sx={{
    mt:2
  }}
  >

  </TextField>
  <Button variant="contained" sx={{
    mt:2,
    mx:"auto",
    width:"30%",
  }} onClick={e=>{if (user?.client.isAuth){
    if (tmpAppeal.typeappeal===""||tmpAppeal.text===""){noDataHandleShow();return;}
    AppealService.AddAppeal(tmpAppeal).then((res)=>{
      if(res){
        handleShow(); 
         setTimeout(()=>{navigate(0)},3000) 
       
      }
    });
    
  }
  else{setOpen(true)}}}>
  Отправить
  </Button>
</FormControl>

    </Box>
    <AuthModal open={open} handlerClose={() => setOpen(false)}/>
    <Snackbar open={show} autoHideDuration={3000} onClose={handleClose}>
        <Alert onClose={handleClose} severity="success" sx={{ width: '100%' }}>
          Ваше обращение успешно отправлено.
        </Alert>
      </Snackbar>
      <Snackbar open={noDataShow} autoHideDuration={3000} onClose={noDataHandleClose}>
        <Alert onClose={noDataHandleClose} severity="warning" sx={{ width: '100%' }}>
          Заполните все поля.
        </Alert>
      </Snackbar>
    </>
  );
}

export default PsixHome;
