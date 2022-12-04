import React, {useState} from "react";
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import { positions } from '@mui/system';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Menu from '@mui/material/Menu';
import Container from '@mui/material/Container';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import Tooltip from '@mui/material/Tooltip';
import MenuItem from '@mui/material/MenuItem';
import {useNavigate} from 'react-router-dom';
import "bootstrap/dist/css/bootstrap.min.css";
import logo from "../assets/img/logo.png";
import "../assets/css/index.css";
import { useSelector} from "react-redux";
import { RootState} from "../redux/store";
import {useDispatch} from "react-redux";
import {AppDispatch} from "../redux/store"
import AuthService from "../redux/services/AuthService"
import AuthModal from "./AuthModal";
import RegisterModal from "./RegisterModal";

        
        
    const pages = ['Products', 'Pricing', 'Blog'];
const settingsDoc = ['Profile', 'Account', 'Dashboard', 'Logout'];

export default function Header() {

 
    const [open, setOpen] = React.useState(false);
    const [regOpen, setRegOpen] = React.useState(false);
    const handleOpen = () => {setOpen(true);}
    const handleRegOpen = () => {setRegOpen(true)}
    const navigate = useNavigate();
    const user = useSelector((state: RootState) => state);
    const dispatch = useDispatch<AppDispatch>();

  const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(null);
  const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);

  const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElNav(event.currentTarget);
  };
  
  const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElUser(event.currentTarget);
  };

  const handleCloseNavMenu = () => {
    setAnchorElNav(null);
  };

  const handleCloseUserMenu = () => {
    setAnchorElUser(null);
  };
    
    return(
        <>
      <AppBar position="static">
      <Container maxWidth="xl">
        <Toolbar disableGutters sx={{
          display:"flex",
          justifyContent:"space-between"
        }}>
          <Box sx={{
            display:"flex"
          }}>
          <img src={logo} width="70px" style={{
                margin:"5px 0 5px 0"
            }}>
            
            </img>
          <Typography
            variant="h6"
            noWrap
            component="a"
            href="/"
            sx={{
              ml: 2,
              mr: 2,
              my:"auto",
              display: { xs: 'none', md: 'flex' },
              fontFamily: 'monospace',
              fontWeight: 700,
              color: 'inherit',
              textDecoration: 'none',
            }}
          >
           Auxilium
          </Typography>
          </Box>
          <Box sx={{ flexGrow: 0, }}>
            {user.client.isAuth?(
              <>
              {user.client.user?.ispsychologist?(
                <Box sx={{ flexGrow: 0 }}>
                <Tooltip title="Open settings">
                  <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
                    <Avatar alt={user.client.user.name} src="/static/images/avatar/2.jpg" />
                  </IconButton>
                </Tooltip>
                <Menu
                  sx={{ mt: '45px' }}
                  id="menu-appbar"
                  anchorEl={anchorElUser}
                  anchorOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                  }}
                  keepMounted
                  transformOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                  }}
                  open={Boolean(anchorElUser)}
                  onClose={handleCloseUserMenu}
                >
                    <MenuItem onClick={()=>{navigate("/")}}>
                      <Typography textAlign="center">Обращения</Typography>
                    </MenuItem>
                    <MenuItem onClick={()=>{navigate("/myanswers")}}>
                      <Typography textAlign="center">Ответы</Typography>
                    </MenuItem>
                    <MenuItem onClick={()=>{dispatch(AuthService.logout());
                    navigate("/")}}>
                      <Typography textAlign="center">Выйти</Typography>
                    </MenuItem>
                  
                </Menu>
              </Box>
              ):(
              <Box sx={{ flexGrow: 0 }}>
                <Tooltip title="Open settings">
                  <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
                    <Avatar alt={user?.client?.user?.name} src="/static/images/avatar/2.jpg" />
                  </IconButton>
                </Tooltip>
                <Menu
                  sx={{ mt: '45px' }}
                  id="menu-appbar"
                  anchorEl={anchorElUser}
                  anchorOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                  }}
                  keepMounted
                  transformOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                  }}
                  open={Boolean(anchorElUser)}
                  onClose={handleCloseUserMenu}
                >
                    <MenuItem onClick={()=>{navigate("/")}}>
                      <Typography textAlign="center">Обратиться</Typography>
                    </MenuItem>
                    <MenuItem onClick={()=>{navigate("/myappeals")}}>
                      <Typography textAlign="center">Мои обращения</Typography>
                    </MenuItem>
                    <MenuItem onClick={()=>{dispatch(AuthService.logout());
                    navigate("/")}}>
                      <Typography textAlign="center">Выйти</Typography>
                    </MenuItem>
                </Menu>
              </Box>
              )}
              </>
              
            ):(
              <Box sx={{
                display:"flex"
              }}>
                <Button variant="contained" color="success" onClick={handleOpen} >
                  Войти
                </Button>
                <Button variant="contained" color="success" onClick={handleRegOpen} sx={{
                  ml:2,
                  mr:2
                }}>
                  Зарегистрироваться
                </Button>
              </Box>
            )
            }
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
    <AuthModal open={open} handlerClose={() => setOpen(false)}/>
    <RegisterModal regOpen={regOpen} regHandlerClose={() => setRegOpen(false)}/>
</>
)
}
