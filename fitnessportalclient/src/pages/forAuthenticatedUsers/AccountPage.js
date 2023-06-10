import classes from "./AccountPage.module.css"

const AccountPage = () => {
    return (
        <div className={classes["acount-main-div"]}>
            <p className={classes["account-header"]}>Account Panel</p>
            <div className={classes["container"]}>
                <div className={classes["personal-div"]}>
                    <p className={classes["panel-label"]}>Personal</p>
                    <p className={classes["property-label"]}>username: Adam Malysz</p>
                    <p className={classes["property-label"]}>email: adam@malysz.pl</p>
                </div>
                <hr className={classes["vertical-line"]} />
                <div className={classes["physical-div"]}>
                    <p className={classes["panel-label"]}>Physical</p>
                    <p className={classes["property-label"]}>date of birth: 10-07-2001</p>
                    <p className={classes["property-label"]}>weight: 77 kg</p>
                    <p className={classes["property-label"]}>height: 187 cm</p>
                </div>
            </div>
            <button className={classes["show-more-button"]}>Update Profile</button>
            </div>
    );
};

export default AccountPage;