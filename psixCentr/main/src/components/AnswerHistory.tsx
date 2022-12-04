import React from 'react';
import { Dayjs } from 'dayjs';
import { LocalizationProvider } from '@mui/x-date-pickers-pro';
import { AdapterDayjs } from '@mui/x-date-pickers-pro/AdapterDayjs';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import "bootstrap/dist/css/bootstrap.min.css"
import Grid from '@mui/material/Grid'
import Box from '@mui/material/Box'
import TextField from '@mui/material/TextField'
import Button from '@mui/material/Button'
import Card from '@mui/material/Card'
import Modal from '@mui/material/Modal';
import Snackbar from '@mui/material/Snackbar';
import Alert from '@mui/material/Alert'; 
import { CardContent, Typography, SelectChangeEvent } from '@mui/material';
import "../assets/css/scrollbar.css"
import AnswerService from '../redux/services/AnswerService';
import { AnswerModel } from '../models/AnswerModel';
import { Dates } from '../models/DatesModel';

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


function AnswerHistory() {

  const [dates,setDates] = React.useState<Dates>({
    startdate:"",
    finishdate:""
  })
  const [successShow,setSuccessShow] = React.useState(false);
    const toggleSuccessShow = () => setSuccessShow(!successShow)
    const [noDataShow,setNoDataShow] = React.useState(false);
    const toggleNoDataShow = () => setNoDataShow(!noDataShow);
  const [answers,setAnswers] = React.useState<AnswerModel[]>([])
    const [startValue, setStartValue] = React.useState<Dayjs | null>(null);
    const [endValue, setEndValue] = React.useState<Dayjs | null>(null);
    const [open,setOpen] = React.useState(false);
    const handleClose = () => setOpen(false);
    const handleOpen = () => setOpen(true);
    const [key,setKey] = React.useState<boolean>(false);
    const [appealType,setAppealType]= React.useState<string>();
    const handleChange = (event: SelectChangeEvent) => {
    setAppealType(event.target.value as string);
  };
  React.useEffect(() => {
    if (key) return;
    AnswerService.getAnswers().then((res)=>{
      if (res !== undefined){
        setAnswers(res)
      }
    })
    setKey(true)
  }, [answers,key])


  return (
<>
<Box sx={{
    mt:2,
    mx:4,
    display:"flex",
    flexDirection:"column",
    alignItems:"center",
}}>
    <Box sx={{
        display:"flex",
        justifyContent:"space-between",
        width:"100%"
    }}>
    <Typography variant="h4" sx={{
    }}>
        Мои ответы
    </Typography>
    <Button variant="contained" onClick={handleOpen} sx={{
    }}>
        Сформировать отчет
    </Button>
    </Box>
    

    <Grid container mt={1} rowSpacing={3} columnSpacing={3} columns={{md: 12}} >
        {answers.map((answer)=>(
          <Grid item md={4}>
            <Card className="scrollbar" sx={{
                height:"200px",
                border:"1px #000 solid",
                overflowY:"auto"
            }}>
                <CardContent>
                    <Typography>
                        <b> Пациент: </b> {answer.appeal.user.surname} {answer.appeal.user.name}
                    </Typography>
                    <Typography >
                        <b> Тип обращения: </b> {answer.appeal.typeappeal}
                    </Typography>
                    <Typography>
                        <b>Дата обращения:</b> {answer.appeal.date}
                    </Typography>
                    <Typography  >
                    <b>Текст обращения: </b>   {answer.appeal.text}
                    </Typography>
                    <Typography variant="body1" sx={{
                        mt:2
                    }}>
                    <b>Ответ:</b>   {answer.answertext}
                    </Typography>
                </CardContent>
            </Card>
        </Grid>
        ))}
        
        
    </Grid>
</Box>
<Modal open={open}
        onClose={handleClose}>
    <Box sx={style}>
        <Typography variant="h5">
            Выберите период для отчета
        </Typography>
    <LocalizationProvider dateAdapter={AdapterDayjs}>
        <Box sx={{
            mt:2    
        }}>
        <DatePicker
        label="Начальная дата"
        value={dates.startdate}
        onChange={(newValue:Dayjs| null) => {setDates({...dates,startdate:newValue!.format('DD/MM/YYYY')})
        }}
        renderInput={(params) => <TextField {...params} 
        />}
      />  
        </Box>
      <Box sx={{
        mt:2,
        mb:2
        }}>
      <DatePicker 
        label="Конечная дата"
        value={dates.finishdate}
        onChange={(newValue:Dayjs| null) => {setDates({...dates,finishdate:newValue!.format('DD/MM/YYYY')})
        }}
        renderInput={(params) => <TextField {...params} />}
      />
      </Box>
      </LocalizationProvider>
      <Button onClick={e=>{if (dates.startdate===""||dates.finishdate===""){setNoDataShow(true);return;}
        AnswerService.MakeReport(dates).then((res)=>{
          if(res){
            handleClose();
            setSuccessShow(true);
          }
        })}} variant="contained">
        Сгенерировать отчет
      </Button>
    </Box>
</Modal>
<Snackbar open={noDataShow} autoHideDuration={5000} onClose={toggleNoDataShow}>
        <Alert onClose={toggleNoDataShow} severity="error" sx={{ width: '100%' }}>
          Заполните все поля!
        </Alert>
      </Snackbar>
      <Snackbar open={successShow} autoHideDuration={5000} onClose={toggleSuccessShow}>
        <Alert onClose={toggleSuccessShow} severity="success" sx={{ width: '100%' }}>
          Отчет отправлен успешно.
        </Alert>
      </Snackbar>
</>
  );
}

export default AnswerHistory;