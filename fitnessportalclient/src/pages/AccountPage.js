import { RequiredAuth } from "../store/authContext";

const AccountPage = () => {
    return (
        <RequiredAuth>
            <h2>Account page</h2>
            <p>To be implemented</p>
        </RequiredAuth>
    );
};

export default AccountPage;