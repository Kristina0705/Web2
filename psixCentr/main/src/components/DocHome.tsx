import React from 'react';
import "bootstrap/dist/css/bootstrap.min.css"
import Grid from '@mui/material/Grid'
import Box from '@mui/material/Box'
import TextField from '@mui/material/TextField'
import Button from '@mui/material/Button'
import Card from '@mui/material/Card'
import Modal from '@mui/material/Modal';
import AppealService from '../redux/services/AppealService';
import Snackbar from '@mui/material/Snackbar';
import {useNavigate} from 'react-router-dom';
import Alert from '@mui/material/Alert'; 
import { Appeal } from '../models/AppealModel';
import { CardContent, Typography, SelectChangeEvent } from '@mui/material';
import "../assets/css/scrollbar.css"
import { PostAnswer } from '../models/AnswerModel';
import AnswerService from '../redux/services/AnswerService';

const style = {
    position: 'absolute' as 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 500,
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4,
    display:"flex",
    flexDirection:"column",
  };

  function DocHome() {

    const navigate = useNavigate();
    const [noDataShow,setNoDataShow] = React.useState(false);
    const noDataHandleShow = () => setNoDataShow(true)
    const noDataHandleClose = () => setNoDataShow(false)
    const [show,setShow] = React.useState(false);
   const showClose = () => setShow(false);
    const handleShow = () => setShow(true);
    const [appeals,setAppeals] = React.useState<Appeal[]>([]);
    const [open,setOpen] = React.useState(false);
    const handleClose = () => setOpen(false);
    const handleOpen = () => setOpen(true);
    const [key,setKey] = React.useState<boolean>(false);
    const [tmpAnswer,setTmpAnswer] = React.useState<PostAnswer>({
        appeal:undefined,
        answertext:""
    })
    const textChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setTmpAnswer({...tmpAnswer,answertext:event.target.value.trim()});
    }
    React.useEffect(() => {
        if (key) return;
        AppealService.getAppeals().then((res)=>{
          if (res !== undefined){
            setAppeals(res)
          }
        })
        setKey(true)
      }, [appeals,key])

    return (
<>
<Box sx={{
    mt:2,
    mx:4,
    display:"flex",
    flexDirection:"column",
    alignItems:"center",
}}>
    <Typography variant="h4" sx={{
    }}>
        Обращения пациентов
    </Typography>
    <Grid container mt={1} rowSpacing={3} columnSpacing={3} columns={{md: 12}} >
        {appeals.map((appeal)=>(
            <Grid item md={4}>
            <Card className="scrollbar" sx={{
                border:"1px #000 solid",
                overflowY:"auto"
            }}>
                <CardContent>
                    <Typography>
                        <b> Пациент: </b> {appeal.user.surname} {appeal.user.name}
                    </Typography>
                    <Typography >
                        <b> Тип обращения: </b> {appeal.typeappeal}
                    </Typography>
                    <Typography >
                        <b>Дата обращения:</b> {appeal.date}
                    </Typography>
                    <Button variant="contained" onClick={e=>{setTmpAnswer({...tmpAnswer,appeal:appeal});handleOpen()}} sx={{
                        mt:2
                    }}>
                        Рассмотреть
                    </Button>
                </CardContent>
            </Card>
        </Grid>
        ))}
        
    </Grid>
</Box>
<Modal open={open}
        onClose={handleClose}>
    <Box sx={style}>
    <Typography>
        <b> Пациент: </b> {tmpAnswer?.appeal?.user?.surname} {tmpAnswer?.appeal?.user?.name}
    </Typography>
    <Typography >
        <b> Тип обращения: </b> {tmpAnswer?.appeal?.typeappeal}
    </Typography>
    <Typography >
        <b>Дата обращения:</b> {tmpAnswer?.appeal?.date}
    </Typography>
      <Typography>
        <b>Текст обращения: </b> {tmpAnswer?.appeal?.text} 
      </Typography>
      <TextField multiline label="Текст ответа" onChange={textChange} fullWidth minRows={7} size="medium" sx={{
    mt:2,
    mb:2
  }}
  >

  </TextField>
  <Button onClick={e=>{
    if(tmpAnswer?.answertext===""){noDataHandleShow();return;}
    AnswerService.AddAnswer(tmpAnswer).then((res)=>{
        if (res){
            handleClose();
            handleShow();
            setTimeout(()=>{navigate(0)},3000) ;
        }
    });

    
  }} variant="contained">
    Ответить
  </Button>
    </Box>
</Modal>
<Snackbar open={noDataShow} autoHideDuration={5000} onClose={noDataHandleClose}>
        <Alert onClose={noDataHandleClose} severity="warning" sx={{ width: '100%' }}>
          Заполните все поля.
        </Alert>
      </Snackbar>
      <Snackbar open={show} autoHideDuration={5000} onClose={showClose}>
        <Alert onClose={showClose} severity="success" sx={{ width: '100%' }}>
          Ответ успешно отправлен.
        </Alert>
      </Snackbar>
</>
 );
}
export default DocHome;