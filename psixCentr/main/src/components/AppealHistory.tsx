import React from 'react';
import "bootstrap/dist/css/bootstrap.min.css"
import Grid from '@mui/material/Grid'
import Box from '@mui/material/Box'
import Card from '@mui/material/Card'
import InputLabel from '@mui/material/InputLabel';
import FormControl from '@mui/material/FormControl';
import { CardContent, MenuItem, Select, Typography, SelectChangeEvent } from '@mui/material';
import "../assets/css/scrollbar.css"
import AppealService from '../redux/services/AppealService';
import { AnswerModel } from '../models/AnswerModel';
import { Appeal } from '../models/AppealModel';
import { TypeAppeal } from '../models/TypeAppealModel';
import { type } from '@testing-library/user-event/dist/type';

function AppealHistory() {

    const [answers,setAnswers] = React.useState<AnswerModel[]>([]);
    const [appeals,setAppeals] = React.useState<Appeal[]>([]);
    const [types,setTypes] = React.useState<TypeAppeal[]>([]);
    const [appealType,setAppealType]= React.useState<string>();
    const [key,setKey] = React.useState<boolean>(false);
    const handleChange = (event: SelectChangeEvent) => {
    setAppealType(event.target.value as string);
  };
  const getAppealsWithParams = (type:string) =>{
    AppealService.filterAppeal(type).then((res:any) => {
        setAnswers(res.answers);
        setAppeals(res.appeals);
        setTypes(res.types);
    })
  }
  React.useEffect(() => {
    if (key) return;
    AppealService.getHistoryAppeal().then((res:any)=>{
      if (res !== undefined){
        setAnswers(res.answers);
        setAppeals(res.appeals);
        setTypes(res.types);

      }
    })
    setKey(true)
  }, [answers,appeals,types,key])
  return (
<>
<Box sx={{
    mt:2,
    mx:4,
    display:"flex",
    flexDirection:"column",
    alignItems:"center",
}}>
    <Typography variant="h4">
        Мои обращения
    </Typography>
    <FormControl sx={{
        width:"25%",
        mt:2
    }}>
    <InputLabel id="appeal">Тип обращения</InputLabel>
    <Select labelId='appeal' label="appeal" value={appealType} onChange={e=>{handleChange(e); getAppealsWithParams(e.target.value)}}>
        <MenuItem value="">
        <em>Тип обращения</em>
        </MenuItem>
        {types?.map((type)=>(
        <MenuItem value={type.TypeName}>
        {type.TypeName}
        </MenuItem>
        ))}
        
       
    </Select>
    </FormControl>
    
    <Grid container mt={1} rowSpacing={3} columnSpacing={3} columns={{md: 12}}>
        {appeals?.map((appeal)=>(
            <Grid item md={4}>
            <Card className="scrollbar" sx={{
                height:"200px",
                border:"1px #000 solid",
                overflowY:"auto"
            }}>
                <CardContent>
                    <Typography >
                        <b> Тип обращения: </b> {appeal.typeappeal}
                    </Typography>
                    <Typography >
                        <b>Дата обращения:</b> {appeal.date}
                    </Typography>
                    <Typography  >
                    <b>Текст обращения: </b>   {appeal.text}
                    </Typography>
                    <Typography variant="body1" sx={{
                        mt:2
                    }}>
                    <b>Не обработано</b>   
                    </Typography>
                </CardContent>
            </Card>
        </Grid>
        ))}
        {answers.map((answer)=>(
            <Grid item md={4}>
            <Card className="scrollbar" sx={{
                height:"200px",
                border:"1px #000 solid",
                overflowY:"auto"
            }}>
                <CardContent>
                    <Typography >
                        <b> Тип обращения: </b> {answer.appeal.typeappeal}
                    </Typography>
                    <Typography >
                        <b>Дата обращения:</b> {answer.appeal.date}
                    </Typography>
                    <Typography  >
                    <b>Текст обращения: </b>   {answer.appeal.text}
                    </Typography>
                    <Typography variant="body1" sx={{
                        mt:2
                    }}>
                    <b>Ответ: </b> {answer.answertext}   
                    </Typography>
                </CardContent>
            </Card>
        </Grid>
        ))}
    </Grid>
</Box>
</>
  );
}

export default AppealHistory;