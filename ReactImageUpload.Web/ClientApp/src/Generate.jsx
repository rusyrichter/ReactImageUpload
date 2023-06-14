import react, {useState} from 'react';
import axios from 'axios';
import { saveAs } from 'file-saver';

const Generate = () => {

    const [amount, setAmount] = useState('');

    const onGenerateClick = async () => {
        const response = await axios.get(`api/people/buildPeopleCsv/${amount}`, {
            responseType: 'blob',
        });
        saveAs(response.data, 'people.csv');
    }

    return (
        <div className="container" style={{ marginTop: "60px" }}>
            <div className="d-flex vh-100" style={{ marginTop: "-70px" }}>
                <div className="d-flex w-100 justify-content-center align-self-center">
                    <div className="row">
                        <input onChange={e => setAmount(e.target.value)} type="text" className="form-control-lg" placeholder="Amount" />
                    </div>
                    <div className="row">
                        <div className="col-md-4 offset-md-2">
                            <button onClick={onGenerateClick} className="btn btn-primary btn-lg">Generate</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}
export default Generate;