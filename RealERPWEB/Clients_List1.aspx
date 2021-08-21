<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="Clients_List1.aspx.cs" Inherits="RealERPWEB.Clients_List1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="introPage ownerpage">
                        
                       <%-- <h3>Few Of our Clients</h3>--%>
                        <%--<table id="example" class="table table-striped table-bordered" cellspacing="0">--%>
                        <table id="example" class="table table-striped table-bordered" cellspacing="0">
                            <thead>
                                <tr>
                                    <th>Logo
                            </th>
                                    <th>Name Of Clients
                            </th>
                                    <th>Contract Name
                            </th>
                                </tr>
                            </thead>
                            <tbody>
                                
                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="GreenBay Logo" src="Images/clientsLogo/Ass.png" /></a>

                                    </th>
                                    <th>
                                        <h6>ASSURE Group Ltd</h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">ASSURE Builders Limited</a>
                                                <a href="#" class="list-group-item">ASSURE Development & Design Limited</a>
                                                <a href="#" class="list-group-item">ASSURE Agro Complex Limited.</a>
                                                <a href="#" class="list-group-item">HR Management</a>
                                                <a href="#" class="list-group-item">Group Reporting</a>
                                                <a href="#" class="list-group-item">JABBAR TOWER, LEVEL-5, 42 Gulshan Avenue, Gulshan Circle-1, Dhaka-1212, Bangladesh.
                                          
                                                    <br />
                                                    Phone:+88-02-8812718, 8112719,8812720,9885763,9850154 Web: www.assuregroup.bd.com</a>
                                            </ul>

                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                       <a href="#" class="list-group-item">Md Baser

                                            <br />
AGM

                                            <br />
                                          
                                          --- </a>
                                    </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="GreenBay Logo" src="Images/clientsLogo/81.png" /></a>

                                    </th>
                                    <th>
                                        <h6>Leisure Bangladesh Limited</h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Leisure Bangladesh Limited</a>
                                                <a href="#" class="list-group-item">Leisure Bangladesh Land Limited</a>
                                                 <a href="#" class="list-group-item">HR Management</a>
                                                <a href="#" class="list-group-item">Group Reporting</a>
                                                <a href="#" class="list-group-item">Road # 95, House # 10 C, (Behind Pink City), Dhaka-1212, Bangladesh.
                                          
                                                    <br />
                                                    (+8802) 9853574-5, (+8802) 9853578 (+88) 01766668444, (+88) 01841119666, www.leisurebd.com Email:info@leisurebd.com.</a>
                                            </ul>

                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                       <a href="#" class="list-group-item">Md Kamrul hasan

                                            <br />
Manager Accounts
                                            <br />
                                          
                                          01677214339 </a>
                                    </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="GreenBay Logo" src="Images/clientsLogo/27.png" /></a>

                                    </th>
                                    <th>
                                        <h6>Greenbay Develpment Ltd</h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Greenbay Develpment Ltd</a>
                                                <a href="#" class="list-group-item">House: 24-C (4th Floor), Road- 16, (old-27) Dhanmondi, Dhaka-1209 
                                          
                                                    <br />
                                                    Phone: 9143889, 9143896, 9143881<br />
                                                    Email:greenbay.develpment@gmail.com,</a>
                                            </ul>

                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Md. Mahiuddin Islam

                                           

                                                    <br />
                                                    Ast. Manager, Accounts
                                           
                                                    <br />

                                                    01625040337 </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="Vicon Logo" src="Images/clientsLogo/25.png" /></a>

                                    </th>
                                    <th>
                                        <h6>Vicon Industries Ltd</h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Vicon Industries Ltd</a>
                                                <a href="http://www.viconbd.com/" class="list-group-item">Cosmic Tower,106/Ka Naya Paltan, Motijheel, Dhaka-1000, 
                                          
                                                    <br />
                                                    Phone-880-2-9341996, 9347025<br />
                                                    Email: info@sohana.com.bd, www.sohana.com.bd</a>
                                            </ul>

                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Mr. Nahid Afser
                                           
                                                    <br />
                                                    Asst. Director-Finance
                                           
                                                   </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="GLG Logo" src="Images/clientsLogo/26.png" /></a>

                                    </th>
                                    <th>
                                        <h6>Greenland Group</h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Greenland Group</a>

                                                <a href="http://www.glandgroup.com/" target="_blank" class="list-group-item">Plot 34, road 100, Gulshan 2. Dhaka
                                          
                                                    <br />
                                                    +8802 888 1836-43 Email: gland@glandgroup.com, www.glandgroup.com</a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Shaharia Sultana Ria
                                            
                                            Manager, Business Development
                                           
                                                    <br />
                                                    +8802 888 1836-43</a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>

                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="Singmar Logo" src="Images/clientsLogo/0.png" /></a>

                                    </th>
                                    <th>
                                        <h6>Singmar Group</h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Singmar Marine and Offshore Pte Ltd </a><a href="#"
                                                    class="list-group-item">Neptune Fleet managers Pte Ltd.</a> <a href="#" class="list-group-item">Avencia Corporation Ltd, Dhaka</a> <a href="#" class="list-group-item">Avencia Corporation Ltd, Ctg</a> <a href="#"
                                                        class="list-group-item">Hygeia Corporation Limited</a> <a href="#" class="list-group-item">HR Management(Group) </a><a href="#" class="list-group-item">Group Reporting</a>
                                                <a href="#" class="list-group-item">#04-307K, 134 Jurong Gateway Road, Singapore Office:
                                            +65-64252271 Email: sales@singmarmarine.com</a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Shah Jewel
                                           
                                                    <br />
                                                    Technical Director
                                           
                                                    <br />
                                                    6591169134
                                        
                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="AsitERP" src="Images/clientsLogo/1.png" /></a>

                                    </th>
                                    <th>
                                        <h6>Rupayan Group</h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Rupayan Housing Ltd </a><a href="#" class="list-group-item">Rupayan Land Ltd </a><a href="#" class="list-group-item">Ratul properties Ltd
                                        </a><a href="#" class="list-group-item">City Uttara </a><a href="#" class="list-group-item">Housing Ctg & Cox-Bazar </a><a href="#" class="list-group-item">Rupayan Holding
                                        </a><a href="#" class="list-group-item">HR Management(Group) </a><a href="#" class="list-group-item">RUPAYAN CENTRE(3rd, 4th, 5th,7th, 9th & 13th,14th, 18th,21th Floor), 72 Mohakhali
                                            C/A Dhaka- 1212 </a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Mr. Mohammad Abul Mannan Molla
                                           
                                                    <br />
                                                    DGM, Cost & Budget
                                           
                                                    <br />
                                                    Mohammad Nurul Haque Shahin
                                           
                                                    <br />
                                                    Incharge ERP & MIS </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>

                                        <a class="thumbnail" href="#">
                                            <img alt="AsitERP" src="Images/clientsLogo/4.png" /></a>

                                    </th>
                                    <th>
                                        <h6>Sanmar Group
                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Sanmar Properties </a><a href="#" class="list-group-item">Sanmar Lands </a><a href="#" class="list-group-item">Sanmar Construction </a>
                                                <a href="#" class="list-group-item">HR Management(Group)</a> <a href="#" class="list-group-item">Sanmar Ocean City (7th Floor), 997 CDA
                                           
                                                    <br />
                                                    Avenue, East Nasirabad,
                                           
                                                    <br />
                                                    Chittagong-4000 </a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">A.K.M. Ahsanul Bari
                                           
                                                    <br />
                                                    Head of Procurment & IT
                                           
                                                    <br />
                                                    Mr. Mostafa
                                           
                                                    <br />
                                                    Asst. Manager, IT </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="AsitERP" src="Images/clientsLogo/2.png" /></a>

                                    </th>
                                    <th>
                                        <h6>Asian Telecast Ltd.
                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Asian Telecast Ltd. </a><a href="#" class="list-group-item">House-60, Road-1, Block-A,
                                           
                                                    <br />
                                                    Nikaton, Gulshan-1,
                                           
                                                    <br />
                                                    Dhaka-1212 </a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">
                                                    Mr. Tariqul Islam
                                           
                                                    <br />
                                                    Manager Accounts </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>  
                                     <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="AsitERP" src="Images/clientsLogo/FCCL.png" /></a>

                                    </th>
                                    <th>
                                        <h6>FCCL 


                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Faruq Consultants and Construction Ltd. 

 
                                        </a>
                                                <a href="#" class="list-group-item">HR Management

 
                                        </a>
                                                <a href="#" class="list-group-item">57/D (First floor), Rd #05, Old DOHS, Banani, Dhaka- 1206. 









 

                                        </a>

                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Mr. Torikul







                                           







                                                    <br />
                                                    Sr. Accounts




                                           




                                                    <br />
                                                    8711668




                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>

                                        <a class="thumbnail" href="#">
                                            <img alt="AsitERP" src="Images/clientsLogo/8.png" /></a>

                                    </th>
                                    <th>
                                        <h6>Suvastu Development Ltd.
                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Suvastu Development Ltd. </a><a href="#" class="list-group-item">Suvastu Imam Square
                                           
                                                    <br />
                                                    (5th Floor),65, Gulshan Avenue,
                                           
                                                    <br />
                                                    Gulshan </a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Mr. Mojibur Rahman
                                           
                                                    <br />
                                                    DGM ( F& A )
                                           
                                                    <br />
                                                    8855261, 8855316 </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="" src="#" /></a>

                                    </th>
                                    <th>
                                        <h6>Akbar Group
                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Akbar Cotton Mills Ltd. </a><a href="#" class="list-group-item">Akbar Composit Ltd. </a><a href="#" class="list-group-item">Akbar Textile Mills Ltd.
                                        </a><a href="#" class="list-group-item">HR Management(Group) </a><a href="#" class="list-group-item">Al-Amin Center, (9th Floor),
                                               
                                                    <br />
                                                    25/A, Dilkusha </a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Mr. Nasir Uddin Ahmed
                                           
                                                    <br />
                                                    GM-Finance
                                           
                                                    <br />
                                                    Mr. Toslim M<br />
                                                    Manager
                                           
                                                    <br />
                                                    7171265, 7176936 </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>

                                        <a class="thumbnail" href="#">
                                            <img alt="" src="#" /></a>

                                    </th>
                                    <th>
                                        <h6>Dressy Deal
                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Ishika Factory </a><a href="#" class="list-group-item">DD-1,U.A.E Complex, Banani</a> <a href="#" class="list-group-item">DD-2,Rupayan Golden
                                                Age, Gulshan </a><a href="#" class="list-group-item">DD-3,S. A. Complex, CTG
                                                </a><a href="#" class="list-group-item">DD-4, Orchard Point 2nd Floor </a><a href="#"
                                                    class="list-group-item"></a><a href="#" class="list-group-item">HR Management
                                                    </a><a href="#" class="list-group-item">(Factory) Ka-70, Progoti Soroni,
                                           
                                                        <br />
                                                        Hazi Ahamed Plaza,Kudil,
                                           
                                                        <br />
                                                        Badda, Dhaka
                                           
                                                        <br>
                                                    </a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Mr. Izaj
                                           
                                                    <br />
                                                    Incharge Inventory & ERP
                                           
                                                    <br />
                                                    Miss. Chaina
                                           
                                                    <br />
                                                    Miss Sumona
                                           
                                                    <br />
                                                    Mr. Lutfor Rahman
                                           
                                                    <br />
                                                    Miss. Dipa
                                           
                                                    <br />
                                                    Mr. Belal IT Incharge
                                           
                                                    <br />
                                                    8414999, 8414975 </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="AsitERP" src="Images/clientsLogo/11.png" /></a>

                                    </th>
                                    <th>
                                        <h6>Foster Group
                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Foster Real Estate Ltd. </a><a href="#" class="list-group-item">Foster Feed Mills </a><a href="#" class="list-group-item">HR Management(Group)
                                        </a><a href="#" class="list-group-item"></a><a href="#" class="list-group-item">Road
                                            # 11, House # 49,
                                           
                                                    <br />
                                                    New D.O.H.S Mohakhali
                                           
                                                    <br />
                                                    Dhaka-1206. </a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Md Asif Mizan
                                           
                                                    <br />
                                                    CEO<br />
                                                    <br />
                                                    8714627, 9861507 </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="AsitERP" src="Images/clientsLogo/7.png" /></a>

                                    </th>
                                    <th>
                                        <h6>Multiplan Limited
                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Multiplan Limited </a><a href="#" class="list-group-item"></a><a href="#" class="list-group-item">Chandrashila Suvastu Tower69/1,
                                           
                                                    <br />
                                                    Panthapath,
                                           
                                                    <br />
                                                    Dhaka-1205 </a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">8814028, 8814030, 8815466 
                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="AsitERP" src="Images/clientsLogo/6.png" /></a>

                                    </th>
                                    <th>
                                        <h6>Metro Homes Ltd. 
                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Metro Homes Ltd. 
                                        </a><a href="#" class="list-group-item"></a><a href="#" class="list-group-item">House # 1, Road # 12, Dhanmondi R/A, 
                                            Dhaka-12059</a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Mr. Rezaul Kabir
                                           
                                                    <br />
                                                    Heads Of Accounts 

                                           

                                                    <br />
                                                    <br />
                                                    8814028, 8814030, 8815466 
                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="AsitERP" src="Images/clientsLogo/5.gif" /></a>

                                    </th>
                                    <th>
                                        <h6>ENA Properties Ltd. 

                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">ENA Properties Ltd. 
                                        </a>
                                                <a href="#" class="list-group-item">57/3, 57/4, Circus Kola Bagan, Panthapath, Dhaka
/a>
                                   
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Mr. Sadequal Haque 

                                           

                                                    <br />
                                                    Heads Of Accounts 

                                           

                                                    <br />
                                                    <br />
                                                    8151281, 9117636, 8116764

                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>


                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="AsitERP" src="Images/clientsLogo/16.gif" /></a>

                                    </th>
                                    <th>
                                        <h6>Nirapad Real Estate Ltd.



                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Nirapad Real Estate Ltd.

                                        </a><a href="#" class="list-group-item"></a><a href="#" class="list-group-item">House # 09, Road # 09, Sector # 09, Uttara, Dhaka-1230 




                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Mr. Atikur Rahman



                                           



                                                    <br />
                                                    Head of Accounts
                                           
                                                    <br />
                                                    <br />
                                                    8963368



                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="AsitERP" src="Images/clientsLogo/14.png" /></a>

                                    </th>
                                    <th>
                                        <h6>Biswas Builders Ltd.




                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Biswas Builders Ltd.


                                        </a><a href="#" class="list-group-item"></a><a href="#" class="list-group-item">44/1, Rahim square, New Market, Dhaka-1205





                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Mr. Mamun




                                           




                                                    <br />
                                                    Head of Accounts
                                           
                                                    <br />
                                                    <br />
                                                    8963368




                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="AsitERP" src="Images/clientsLogo/20.png" /></a>

                                    </th>
                                    <th>
                                        <h6>Living Stone Ltd.

                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Living Stone Ltd.
                                        </a><a href="#" class="list-group-item"></a><a href="#" class="list-group-item">House # 50 (Ground floor), Rd # 02, Old DOHS, Banani, Dhaka- 1206.

                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Mr. Waresul Haque Khan
                                           
                                                    <br />
                                                    DGM ( F& A )

                                           

                                                    <br />
                                                    <br />
                                                    8753296, 8753297, 8753298





                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="AsitERP" src="Images/clientsLogo/12.png" /></a>

                                    </th>
                                    <th>
                                        <h6>Nagar Homes Ltd. 


                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Nagar Homes Ltd. 

                                        </a><a href="#" class="list-group-item"></a><a href="#" class="list-group-item">House # 37, BL # G, Road # 07, Banani, Dhaka- 1213




                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Mr. Faruque 

                                           

                                                    <br />
                                                    Manager Accounts 

                                           

                                                    <br />
                                                    <br />
                                                    8836617-9






                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="" src="Images/clientsLogo/00.png" /></a>

                                    </th>
                                    <th>
                                        <h6>Radiance Group 





                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Radiance-1</a>
                                                <a href="#" class="list-group-item">Radiance-2</a>

                                                <a href="#" class="list-group-item"></a><a href="#" class="list-group-item">House#521, Road#10 DOSH, Baridhara, Dhaka



                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">A.S.M. Shaykhul Islam FCMA 





                                           





                                                    <br />
                                                    Finance Director 

                                           

                                                    <br />
                                                    <br />
                                                    8832151, 8829371 





                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="" src="Images/clientsLogo/00.png" /></a>

                                    </th>
                                    <th>
                                        <h6>GlaxoSmithKline Bangladesh Ltd. 




                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">GlaxoSmithKline Bangladesh Ltd. 


                                        </a><a href="#" class="list-group-item"></a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Mr. Farque Hossain, FCMA


                                           


                                                    <br />
                                                    Management Accountant 



                                           



                                                    <br />
                                                    <br />
                                                    8858870-5



                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="AsitERP" src="Images/clientsLogo/21.jpg" /></a>

                                    </th>
                                    <th>
                                        <h6>Multiplan Development Ltd.



                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Multiplan Development Ltd.


                                        </a><a href="#" class="list-group-item"></a><a href="#" class="list-group-item">Chandrashila Suvastu Tower69/1, 12Th Floor, Panthapath,Dhaka-1205.







                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Mr. Shafiruddin 


                                           


                                                    <br />
                                                    Head Of Accounts 


                                           


                                                    <br />
                                                    <br />
                                                    9677559, 9677560, 8612709


                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <%--<tr>
                            <th></th>
                            <th>
                                <h6>Hospital Management 




                                </h6>
                                <div class="panel panel-default">
                                    <ul class="list-group">
                                        <a href="#" class="list-group-item">Digilab Diagonostic Ltd.</a><a href="#" class="list-group-item">Dr. Azmal Hospital Ltd. 
</a>
                                        <a href="#" class="list-group-item"></a>
                                        
                                    </ul>
                                </div>
                            </th>
                            <th>
                                <div class="panel panel-default contractName">
                                    <ul class="list-group">
                                        <a href="#" class="list-group-item">Mr. Yousuf



                                            <br />
                                          Head of Accounts 



                                            <br />
                                            <br />
                                          8053469



                                        </a>
                                    </ul>
                                </div>
                            </th>
                        </tr>--%>
                                <tr>
                                    <th></th>
                                    <th>
                                        <h6>Govt. Work 




                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Application Software for DLRS 
                                        </a><a href="#" class="list-group-item">Plastic Driving License, BRTA (1999-2006)

                                        </a>
                                                <a href="#" class="list-group-item"></a>

                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Mr. Tito Chowdhury 




                                           




                                                    <br />
                                                    Manager 




                                           




                                                    <br />
                                                    <br />
                                                    8714627, 9861507 




                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="" src="Images/clientsLogo/15.png" /></a>

                                    </th>
                                    <th>
                                        <h6>Excel Real Estate 
                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Excel Real Estate 
 
                                        </a>
                                                <a href="#" class="list-group-item">Bengal Centre (6th Floor) 28, Topkhana Road. Dhaka-1000. 
 

                                        </a>

                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Mrs Nazia





                                           





                                                    <br />
                                                    Sr. Accounts 




                                           




                                                    <br />
                                                    <br />
                                                    7112104




                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="" src="Images/clientsLogo/13.png" /></a>

                                    </th>
                                    <th>
                                        <h6>Probashi Bangla Development Ltd. 

                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Probashi Bangla Development Ltd. 

 
                                        </a>
                                                <a href="#" class="list-group-item">Gazi Tower (5th Floor), 151/6 Green Road, Dhaka-1205. 



 

                                        </a>

                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Mr. Rakib






                                           






                                                    <br />
                                                    Head Of Accounts




                                           




                                                    <br />
                                                    <br />
                                                    8141625




                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th></th>
                                    <th>
                                        <h6>Deveopment Ecotecture Ltd. 


                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Deveopment Ecotecture Ltd. 

 
                                        </a>
                                                <a href="#" class="list-group-item">57/D (First floor), Rd #15A (New), Dhanmondi R/A, Dhaka- 1205 






 

                                        </a>

                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Mr. Ismat







                                           







                                                    <br />
                                                    Accounts 




                                           




                                                    <br />
                                                    <br />
                                                    8191388




                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th></th>
                                    <th>
                                        <h6>Minimax Ltd.



                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Minimax Ltd.

 
                                        </a>
                                                <a href="#" class="list-group-item">Amir Complex, Uttara, Dhaka


                                        </a>

                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Not in Action
                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>

                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="" src="Images/clientsLogo/23.jpg" /></a>

                                    </th>
                                    <th>
                                        <h6>Digilab Medical Services Ltd. 

 


                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Digilab Medical Services Ltd.
. 

 
                                        </a>
                                                <a href="#" class="list-group-item">House#2, Road#6, Block#A, Mirpur, Dhaka











 

                                        </a>

                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Md. Kader

                                           

                                                    <br />
                                                    Accounts

                                           

                                                    <br />
                                                    8810753






                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="" src="Images/clientsLogo/22.jpg" /></a>

                                    </th>
                                    <th>
                                        <h6>Amir Group
                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Sagore Garments Ltd. </a><a href="#" class="list-group-item">Minimax Garments Ltd. </a><a href="#" class="list-group-item">Optimum Garments
                                        </a><a href="#" class="list-group-item">The Accessories Ltd., Dhoha, EPZ </a><a href="#"
                                                    class="list-group-item"></a><a href="#" class="list-group-item">Amir Complex,
                                               
                                                        <br />
                                                        5th-6th Floor,
                                               
                                                        <br />
                                                        Uttara, Dhaka-1230 </a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Not In Account</a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                        <a class="thumbnail" href="#">
                                            <img alt="" src="Images/clientsLogo/10.png" /></a>

                                    </th>
                                    <th>
                                        <h6>National Development Engineers Ltd (NDE)

                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">National Development Engineers Ltd


 
                                        </a>
                                                <a href="#" class="list-group-item">House#20/A, Road#44, Gulshan, Dhaka







 

                                        </a>

                                            </ul>
                                        </div>
                                    </th>

                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Mr. Noman
                                           
                                                    <br />
                                                    Accounts
                                       
                                                    <br />
                                                    8053469



                                        </a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                                <tr>
                                    <th>

                                        <a class="thumbnail" href="#">
                                            <img alt="" src="Images/clientsLogo/00.png" /></a>

                                    </th>
                                    <th>
                                        <h6>Concord Group
                                </h6>
                                        <div class="panel panel-default">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Concord Real Estate Ltd. </a><a href="#" class="list-group-item">Concord Ready-Mix Ltd. </a><a href="#" class="list-group-item">Concord Real Estate &
                                                Dev. Ltd. </a><a href="#" class="list-group-item">Concord Block Plant Ltd.
                                                </a><a href="#" class="list-group-item">Concord Tiles Plant Ltd. </a><a href="#"
                                                    class="list-group-item">Concord Trading Company Ltd. </a><a href="#" class="list-group-item">Jeacon Garments Ltd. </a><a href="#" class="list-group-item">Concord Fashion Export
                                                    Ltd. </a><a href="#" class="list-group-item">HR Management(Group) </a>
                                                <a href="#" class="list-group-item">43 Gulshan, Concord Center,
                                           
                                                    <br />
                                                    Gulshan, Dhaka </a>
                                            </ul>
                                        </div>
                                    </th>
                                    <th>
                                        <div class="panel panel-default contractName">
                                            <ul class="list-group">
                                                <a href="#" class="list-group-item">Not In Account</a>
                                            </ul>
                                        </div>
                                    </th>
                                </tr>
                            </tbody>
                        </table>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

