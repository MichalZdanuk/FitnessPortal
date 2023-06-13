import classes from "./AccountPageLayout.module.css"
import { RequiredAuth } from "../../store/authContext"
import { Outlet } from "react-router-dom"
import LeftSidePanel from "../../components/account/LeftSidePanel"

const AccountPageLayout = () => {
    return(
    <RequiredAuth>
        <div className={classes["container"]}>
            <LeftSidePanel/>
            {/* <div className={classes["left-panel-div"]}><LeftSidePanel/></div> */}
                <Outlet/>
        </div>
    </RequiredAuth>
    )
}

export default AccountPageLayout