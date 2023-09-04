/*This file is part of Enzigame.
Enzigame is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as 
published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Enzigame is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Foobar.  If not, see <https://www.gnu.org/licenses/>6.*/

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem {

    public static void SavePlayer (ScoreManager scoreScript){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player";
        FileStream stream = new FileStream(path,FileMode.Create);

        HighestScoreData data = new HighestScoreData(scoreScript);

        formatter.Serialize(stream, data);
        stream.Close(); 
    }

    public static HighestScoreData LoadPlayer (){
        string path = Application.persistentDataPath + "/player";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            HighestScoreData data = formatter.Deserialize(stream) as HighestScoreData;
            stream.Close();

            return data;
        }else{
            Debug.LogError("SavePlayer file not found in" + path);
            return null;
        }
    }   
}
