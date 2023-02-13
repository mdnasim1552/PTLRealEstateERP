<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="DeafultMenu.aspx.cs" Inherits="RealERPWEB.DeafultMenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
    <style>
        /*Now the CSS*/
        * {
            margin: 0;
            padding: 0;
        }

        .accImg {
            margin: 0 auto;
        }

        .tree {
            margin: 0 auto;
        }

            .tree ul {
                padding-top: 20px;
                padding-bottom: 20px;
                position: relative;
                transition: all 0.5s;
                -webkit-transition: all 0.5s;
                -moz-transition: all 0.5s;
            }

            .tree li {
                float: left;
                text-align: center;
                list-style-type: none;
                position: relative;
                padding: 20px 5px 0 5px;
                transition: all 0.5s;
                -webkit-transition: all 0.5s;
                -moz-transition: all 0.5s;
            }

                /*We will use ::before and ::after to draw the connectors*/

                .tree li::before, .tree li::after {
                    content: '';
                    position: absolute;
                    top: 0;
                    right: 50%;
                    border-top: 1px solid #ccc;
                    width: 50%;
                    height: 20px;
                }

                .tree li::after {
                    right: auto;
                    left: 50%;
                    border-left: 1px solid #ccc;
                    /*background:url(image/LongRightArrow_L.gif) no-repeat -10px center;*/
                    z-index: 500;
                }

                /*We need to remove left-right connectors from elements without 
any siblings*/
                .tree li:only-child::after, .tree li:only-child::before {
                    display: none;
                }

                /*Remove space from the top of single children*/
                .tree li:only-child {
                    padding-top: 0;
                }

                /*Remove left connector from first child and 
right connector from last child*/
                .tree li:first-child::before, .tree li:last-child::after {
                    border: 0 none;
                }
                /*Adding back the vertical connector to the last nodes*/
                .tree li:last-child::before {
                    border-right: 1px solid #ccc;
                    border-radius: 0 5px 0 0;
                    -webkit-border-radius: 0 5px 0 0;
                    -moz-border-radius: 0 5px 0 0;
                }

                .tree li:first-child::after {
                    border-radius: 5px 0 0 0;
                    -webkit-border-radius: 5px 0 0 0;
                    -moz-border-radius: 5px 0 0 0;
                }

            /*Time to add downward connectors from parents*/
            .tree ul ul::before {
                content: '';
                position: absolute;
                top: 0;
                left: 50%;
                border-left: 1px solid #ccc;
                width: 0;
                height: 20px;
            }

            .tree li a {
                font-family: Cambria;
                -moz-box-shadow: inset 0px 1px 0px 0px #ffffff;
                -webkit-box-shadow: inset 0px 1px 0px 0px #ffffff;
                box-shadow: inset 0px 1px 0px 0px #ffffff;
                background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #f9f9f9), color-stop(1, #e9e9e9));
                background: -moz-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
                background: -webkit-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
                background: -o-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
                background: -ms-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
                background: linear-gradient(to bottom, #f9f9f9 5%, #e9e9e9 100%);
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#f9f9f9', endColorstr='#e9e9e9',GradientType=0);
                background-color: #f9f9f9;
                -moz-border-radius: 6px;
                -webkit-border-radius: 6px;
                border-radius: 6px;
                border: 1px solid #dcdcdc;
                display: inline-block;
                cursor: pointer;
                color: #000000;
                font-weight: bold;
                font-size: 18px;
                margin: auto;
                padding: 12px 5px;
                width: 175px;
                text-decoration: none;
                transition: all 0.5s ease;
            }

                /*Time for some hover effects*/
                /*We will apply the hover effect the the lineage of the element also*/
                .tree li a:hover {
                    background: #F1FEDD;
                    background-image: -webkit-linear-gradient(top, #F1FEDD, #DBFDA8);
                    background-image: -moz-linear-gradient(top, #F1FEDD, #DBFDA8);
                    background-image: -ms-linear-gradient(top, #F1FEDD, #DBFDA8);
                    background-image: -o-linear-gradient(top, #F1FEDD, #DBFDA8);
                    background-image: linear-gradient(to bottom, #F1FEDD, #DBFDA8);
                    color: #000;
                }
                    /*Connector styles on hover*/
                    .tree li a:hover + ul li::after,
                    .tree li a:hover + ul li::before,
                    .tree li a:hover + ul::before,
                    .tree li a:hover + ul ul::before {
                        border-color: #94a0b4;
                    }

        .commmonTree {
            /*width: 60%;*/
            margin: 0 auto;
        }

            .commmonTree .titelPanel {
                -moz-box-shadow: inset 0px 1px 0px 0px #ffffff;
                -webkit-box-shadow: inset 0px 1px 0px 0px #ffffff;
                box-shadow: inset 0px 1px 0px 0px #ffffff;
                background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #f9f9f9), color-stop(1, #e9e9e9));
                background: -moz-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
                background: -webkit-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
                background: -o-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
                background: -ms-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
                background: linear-gradient(to bottom, #f9f9f9 5%, #e9e9e9 100%);
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#f9f9f9', endColorstr='#e9e9e9',GradientType=0);
                background-color: #f9f9f9;
                -moz-border-radius: 6px;
                -webkit-border-radius: 6px;
                border-radius: 6px;
                border: 1px solid #dcdcdc;
                display: inline-block;
                cursor: pointer;
                color: #000000;
                font-family: arial;
                font-size: 24px;
                font-weight: bold;
                width: 200px;
                padding: 20px 12px;
                text-decoration: none;
                text-shadow: 0px 1px 0px #ffffff;
            }

                .commmonTree .titelPanel:hover {
                    background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #e9e9e9), color-stop(1, #f9f9f9));
                    background: -moz-linear-gradient(top, #e9e9e9 5%, #f9f9f9 100%);
                    background: -webkit-linear-gradient(top, #e9e9e9 5%, #f9f9f9 100%);
                    background: -o-linear-gradient(top, #e9e9e9 5%, #f9f9f9 100%);
                    background: -ms-linear-gradient(top, #e9e9e9 5%, #f9f9f9 100%);
                    background: linear-gradient(to bottom, #e9e9e9 5%, #f9f9f9 100%);
                    filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#e9e9e9', endColorstr='#f9f9f9',GradientType=0);
                    background-color: #e9e9e9;
                }





            .commmonTree .smllMenuBox {
                margin-right: 40px;
                padding: 12px;
                width: 150px !important;
            }

            .commmonTree .smllMenuBox1 {
                margin-left: 40px;
                padding: 12px;
                width: 150px !important;
            }

        .treeCenter {
            width: 100%;
        }

        .treeCenter2 {
            width: 74%;
        }

        .treePnlBudgt {
            width: 58%;
        }

        .treePnlProd {
            width: 58%;
        }

        .treePnlPur {
            width: 85%;
        }

        .menuImg {
            width: 15px;
            height: 15px;
            margin-right: 10px;
            border-radius: 18%;
            border: 1px solid #85e11e;
            padding: 0;
        }

        .flowMenu ul {
            margin-left: 25px;
        }

            .flowMenu ul li {
                list-style: none;
                padding: 5px 0;
                /*border-bottom: 1px solid #e9e9e9;*/
            }

                .flowMenu ul li a {
                    padding-bottom: 8px;
                    color: #000;
                    font-size: 14px;
                    font-weight: normal;
                    text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
                    font-family: 'Times New Roman';
                }

        .flowMenu h3 {
            background: #046971;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
            color: #fff;
            font-family: AR CENA;
            font-size: 18px;
            /*font-weight: bold;*/
            line-height: 40px;
            margin: 5px 0 0;
            padding: 0 0;
            text-decoration: none;
            text-align: center;
        }




        .fadeInRightBig h3 {
            /*/* background: #232526; */
            /* background: -webkit-linear-gradient(to left, #232526 , #414345); */
            /* background: linear-gradient(to left, #232526 , #414345); */
            /* color: #fff; */
            /* font-family: 'Agency FB'; */
            /* font-size: 24px; */
            /* font-weight: bold; */
            /* line-height: 40px; */
            /* margin: 5px 0 0; */
            /* text-align: center; */
            /* text-decoration: underline; */
            font-family: 'Agency FB';
            background: #046971;
            /* border: 1px solid #699f44; */
            /* box-shadow: 0 0 4px 2px #bec9b6 inset; */
            color: #fff;
            font-family: "Agency FB";
            font-size: 22px;
            line-height: 40px;
            margin: 5px -3px 0;
            text-align: center;
            text-decoration: underline;
            font-weight: normal;
            */;
        }
        /*Thats all. I hope you enjoyed it.
Thanks :)*/
        ul.dashCir {
            width: 75%;
            margin: 0 auto;
        }

            ul.dashCir li {
                width: 30%;
                float: left;
                margin: 0 3px;
                border: none;
            }

            ul.dashCir span img {
                border: 1px solid #85e11e;
                border-radius: 50%;
                display: block;
                height: 50px;
                margin: 0 auto;
                padding: 5px;
                width: 50px;
            }

        .dashCir li a {
            display: block;
            text-align: center;
            font-family: AR CENA !important;
            font-size: 14px !important;
        }

        .nrbt input[type="checkbox"], input[type="radio"] {
            display: block;
            width: 100px;
            height: 60px;
            border: 1px solid #85e11e;
            border-radius: 50%;
        }

        .nrbt label {
            color: #000;
            display: block;
            text-align: center;
        }

        .allpagebg {
            background-image: url("Image/bg.PNG") !important;
            overflow: hidden;
        }
        .hover-item li a:hover {
            color: #09A9E2;
        }
        .hover-item li a {
            text-transform: capitalize;
        }
        .hrreport {
            padding-bottom: 8px;
            color: #000;
            font-size: 14px;
            font-weight: normal;
            text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
            font-family: 'Times New Roman';
        }
        .ComName {
            color: #000;
            font-size: 14px;
            font-weight: normal;
            text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
            font-family: 'Times New Roman';
        }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        var comcod;
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {

            GetMenu();
        }

        function navFromList( comcod )
        {
           if (comcod == "0000") {
               return false;
           }
            window.open('CompanyOverAllReport.aspx?comcod='+ comcod,'_blank');
            return false;
        }



        function GetMenu() 
        {
            

            try {
                comcod = <%=this.GetCompCode()%>;
                $("#hlmember").attr("href","F_81_Hrm/F_92_Mgt/AllEmpList.aspx?Type=Report&comcod="+comcod);
                //$("#lblpurchase").attr("href","F_14_Pro/PurInformation.aspx?Type=Report&comcod="+comcod);
                //$("#lblcons").attr("href","F_08_PPlan/ConstructionInfo.aspx?Type=Report&comcod="+comcod);
                //$("#lblaccount").attr("href","F_18_MAcc/AccDashBoard.aspx?Type=Report&comcod="+comcod);
                //$("#lblbbalance").attr("href","F_17_Acc/AccTrialBalance.aspx?Type=BankPosition&comcod="+comcod);
                //$("#lbldues").attr("href","F_99_Allinterface/SalesInterface.aspx?Type=Report&comcod="+comcod);
                //$("#lblmanpower").attr("href","F_22_Sal/RptSaleSoldunsoldUnit.aspx?Type=soldunsold&comcod='"+comcod+"'&prjcode=");
                //$("#lblsalary").attr("href","F_32_Mis/RptMisMasterBgd.aspx?Type=InvPlan&comcod="+comcod);
                //$("#actinterface").attr("href","F_99_Allinterface/AccountInterface.aspx?Type=Report&comcod="+comcod);
                //$("#purinter").attr("href","F_99_Allinterface/RptPurInterface.aspx?Type=Report&comcod="+comcod);
                //$("#salesinter").attr("href","F_99_Allinterface/SalesInterface.aspx?Type=Report&comcod="+comcod);
                //$("#coninter").attr("href","F_99_Allinterface/SubContractorBillInterface.aspx?Type=Report&comcod="+comcod);
      
                $.ajax({
                    type: "POST",
                    url: "DeafultMenu.aspx/CallCompanyList",
                    data: '{comcod:"'+comcod+'"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(response) {
                        //console.log(JSON.parse(response.d));
                        var data = response.d;

                       // console.log(data);
                        Menulist(data);
                    },
                    failure: function(response) {
                        //  alert(response);
                        alert("f");
                    }
                });
            } catch (e) {
                alert(e);
            };

        }

        function Menulist(bgd) {

            try {

                var data = JSON.parse(bgd);
                
                var leftul = $("#leftul");
                leftul.html('');
                var sl = 1;
                $.each(data,function(i, item) {
                    leftul.append('<li class="hover-item"><a href=' +encodeURI("CompanyOverAllReport.aspx?comcod=" + item.comcod) +' target=_blank >'+sl.toString()+". " +item.comsnam+'</a></li>');

                    sl++;
                });

                leftul.show();

            } catch (e) {

            }
        }
    </script>
    <div class="container  moduleItemWrp defaultMenuPart ">
        <div class="row">
            <div class="col-md-12">

                

               

                <asp:Panel runat="server" ID="Panel8" Visible="false">
                    <div class="row flowMenu">

                        <div class="col-md-4">
                            <h3>General Reports</h3>
                            <ul>
                               <%-- <li><span>
                                    <img class="menuImg" src="Image/LP.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=01")%>" target="_blank">Business Planning</a></li>--%>
                              
                                
                                
                                  <%-- <li><span>
                                    <img class="menuImg" src="Image/PF.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=pfpanal")%>">Project Feasibility</a></li>--%>
                                <li><span>
                                    <img class="menuImg" src="Image/B1.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=02")%>" target="_blank">Land Data Bank</a></li>
                                <%-- <li><span>
                                    <img class="menuImg" src="Image/PP.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=pppanal")%>">Project Planning</a></li>--%>
                               <%-- <li><span>
                                    <img class="menuImg" src="Image/CONS.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=03")%>" target="_blank">Pre-Construction Plannig</a></li>--%>
                                <li><span>
                                    <img class="menuImg" src="Image/CONS.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=04")%>" target="_blank">Budgetary Control</a></li>
                               <%-- <li><span>
                                    <img class="menuImg" src="Image/CC.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=06")%>" target="_blank">Project Planning</a></li>--%>


                                <li><span>
                                    <img class="menuImg" src="Image/S1.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=07")%>" target="_blank">Project Implementation</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/CR.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=08")%>" target="_blank">Inventory</a></li>

                              <%--  <li><span>
                                    <img class="menuImg" src="Image/FIN.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=09")%>" target="_blank">Central Warehouse</a></li>--%>
                                <li><span>
                                    <img class="menuImg" src="Image/ACC.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=10")%>" target="_blank">Procurement</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/ABP.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=11")%>" target="_blank">Accounts</a></li>
                                <%--<li><span>
                                    <img class="menuImg" src="Image/SO.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=12")%>" target="_blank">Project</a></li>--%>



<%--                                <li><span>
                                    <img class="menuImg" src="Image/20.jpg" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=14")%>" target="_blank">Marketing</a></li>--%>

                                <li><span>
                                    <img class="menuImg" src="Image/3.jpg" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=15")%>" target="_blank">Sales</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/17.jpg" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=16")%>" target="_blank">Credit Realization(CR)</a></li>
                                <%--<li><span>
                                    <img class="menuImg" src="Image/13.jpg" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=17")%>" target="_blank">Customer Care</a></li>--%>

                                <li><span>
                                    <img class="menuImg" src="Image/13.jpg" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=19")%>" target="_blank">Fixed Assets</a></li>


                                <li><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=21")%>" target="_blank">MIS</a></li>


                            </ul>

                        </div>
                        <div class="col-md-4">
                            <h3>DashBoard</h3>
                            <ul class="dashCir">
                                <li><a href="<%=this.ResolveUrl("~/F_22_Sal/SalesInformation.aspx")%>" target="_blank"><span>
                                    <img class="" src="Image/S2.png" /></span>Sales</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurInformation.aspx")%>" target="_blank"><span>
                                    <img class="" src="Image/procurment1.png" /></span>Procurement</a></li>
                                <li><a href="<%=this.ResolveUrl("F_08_PPlan/ConstructionInfo.aspx")%>" target="_blank"><span>
                                    <img class="" src="Image/CONS2.png" /></span>Construction</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_18_MAcc/AccDashBoard.aspx")%>" target="_blank"><span>
                                    <img class="" src="Image/acc3.png" /></span>Accounts</a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_32_Mis/RptMisMasterBgd.aspx?Type=InvPlan")%>" target="_blank"><span>
                                    <img class="" src="Image/proj3.png" /></span>Project</a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_46_GrMgtInter/RptGrpDailyReportJq.aspx")%>" target="_blank"><span>
                                    <img class="" src="Image/OV.png" /></span>Overall</a></li>


                                 <li><a href="<%=this.ResolveUrl("~/F_32_Mis/RptManProjectSum.aspx")%>" target="_blank"><span>
                                    <img class="" src="Image/OV.png" /></span>Project Summary</a></li>

                                   


                            </ul>
                            <div class="clearfix"></div>
                        </div>
                        <div class="col-md-4">
                            <h3>HR Reports</h3>
                            <ul>
                                 <li><span>
                                    <img class="menuImg" src="Image/LP.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/AllEmpList.aspx?Type=Report")%>">Members</a></li>
                               
                                <li><span>
                                    <img class="menuImg" src="Image/PF.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_97_MIS/RptMgtInterface.aspx")%>">Summary</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/B1.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_99_MgtAct/RptgroupAttendance?Type=Dept")%>">Attendance</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/B1.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/RptWeekPresence.aspx")%>">Attendance(W)</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/FIN.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Salary&Entry=Payroll")%>">Salary Sheet</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/PP.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RptSalarySummary.aspx?Type=SalSum")%>">Salary Sum</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/PP.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/EmpBankSalary.aspx?Type=Entry")%>">Forwarding</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/LP.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Payslip")%>">Payslip</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/CC.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/RptAttendenceSheet.aspx")%>">Attendance Information</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/CONS.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptEmpInformation.aspx?Type=EmpAllInfo")%>">Profile</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/MAR.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_84_Lea/RptEmpLeaveStatus02.aspx?Type=EmpLeaveStatus")%>">Leave Status (Dept)</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/MAR.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_85_Lon/EmpLoanStatus.aspx")%>">Loan</a></li>
                                <%-- <li><span>
                                    <img class="menuImg" src="Image/MS.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=mspanal")%>">Materials Store</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/MAR.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=Marketpanal")%>">Marketing</a></li>--%>
                                <li><span>
                                    <img class="menuImg" src="Image/S1.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=EmpCon")%>">Confirmation</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/CR.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=SepType")%>">Separation</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/CC.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_97_MIS/RptNewJoingInfo.aspx")%>">Joining</a></li>


                                <%-- %> <li><span>
                                    <img class="menuImg" src="Image/ACC.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Salary&Entry=Payroll")%>">ACR</a></li>--%>

                                <li><span>
                                    <img class="menuImg" src="Image/ABP.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptMyInterface.aspx?empid=")%>">My Interface</a></li>


                            </ul>
                            <%--<ul>
                               
                                <li><span>
                                    <img class="menuImg" src="Image/FLOW.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=8010")%>">Flow Chart</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/SE.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4110")%>">Settings</a></li>





                                <li><span>

                                    <img class="menuImg" src="Image/16.jpg" /></span>
                                    <asp:LinkButton ID="lnkbtnGeneral" runat="server" OnClick="lnkbtnGeneral_Click">General</asp:LinkButton>


                                </li>

                                <li><span>

                                    <img class="menuImg" src="Image/16.jpg" /></span>
                                    <asp:LinkButton ID="lnkbtnHr" runat="server" OnClick="lnkbtnHr_Click">HR Management</asp:LinkButton>


                                </li>


                                <li><span>

                                    <img class="menuImg" src="Image/16.jpg" /></span>
                                    <asp:LinkButton ID="lnkbtnKPI" runat="server" OnClick="lnkbtnKPI_Click">KPI</asp:LinkButton>


                                </li>




                                <li><span>
                                    <img class="menuImg" src="Image/GRP.png" /></span><a href="<%=this.ResolveUrl("~/")%>">Group Information</a></li>

                            </ul>--%>



                            <%--                                    <img class="menuImg" src="Image/16.jpg" />
                                    <asp:LinkButton ID="lnkbtnHr" runat="server" OnClick="lnkbtnHr_Click">HR Management</asp:LinkButton>
                            --%>
                        </div>
                    </div>

                    <div class="clearfix"></div>
                </asp:Panel>

                <asp:Panel runat="server" ID="PanelAllins" Visible="false">
                    <div class="row flowMenu">

                        <div class="col-md-4">
                            <h3>Modules</h3>
                            <ul>
                                <li><span><img class="menuImg" src="Image/LP.png" /></span>
                                    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="lnkbtnAbp_Click"> Business Plannning </asp:LinkButton></li>

                                <li><span><img class="menuImg" src="Image/PF.png" /> </span>
                                    <asp:LinkButton ID="LinkButton8" runat="server" OnClick="LinkButton8_Click">Land Procurement</asp:LinkButton></li>

                                <li><span><img class="menuImg" src="Image/B1.png" /></span>
                                    <asp:LinkButton ID="lnkbtnRowMatInventory" runat="server" OnClick="lnkbtnMatr_Click">Budgetary Control</asp:LinkButton></li>

                                <li><span><img class="menuImg" src="Image/CONS.png" /></span>
                                    <asp:LinkButton ID="lnkbtnpurch" runat="server" OnClick="lnkbtnPlaning_Click"> Project Planing </asp:LinkButton></li>

                                <li><span><img class="menuImg" src="Image/CC.png" /> </span>
                                    <asp:LinkButton ID="linkbtnprodMin" runat="server" OnClick="btnImp_Click">Project Implementation</asp:LinkButton></li>

                                <li><span><img class="menuImg" src="Image/S1.png" /></span>
                                    <asp:LinkButton ID="lnkbtnGInventory" runat="server" OnClick="lnkbtnGoodsInv_Click">Inventory Control </asp:LinkButton></li>

                                <li>
                                    <span>
                                        <img class="menuImg" src="Image/FIN.png" /></span>
                                    <asp:LinkButton ID="inkbtnCW" runat="server" OnClick="inkbtnCW_Click">Central Warehouse</asp:LinkButton></li>

                                
                                <li><span>
                                    <img class="menuImg" src="Image/ACC.png" /></span>
                                    <asp:LinkButton ID="LinkButton7" runat="server" OnClick="btnPur_Click">Procurement Module </asp:LinkButton></li>


                                <li><span>
                                    <img class="menuImg" src="Image/ABP.png" /></span>
                                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="lnkbtnACC_Click">General Accounts</asp:LinkButton></li>

                                <li><span>
                                    <img class="menuImg" src="Image/SO.png" /></span>
                                    <asp:LinkButton ID="LinkButton75" runat="server" OnClick="lnkbtnMKT_Click">Marketing</asp:LinkButton></li>

                                <li><span>
                                    <img class="menuImg" src="Image/20.jpg" /></span>
                                    <asp:LinkButton ID="lnkbtnSale" runat="server" OnClick="lnkbtnSale_Click">Sales</asp:LinkButton></li>

                                <li><span>
                                    <img class="menuImg" src="Image/3.jpg" /></span>
                                    <asp:LinkButton ID="LinkButton6" runat="server" OnClick="lnkbtnCR_Click"> Credit Realization </asp:LinkButton></li>

                                <li><span>
                                    <img class="menuImg" src="Image/CR.png" /></span>
                                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lnkbtnCC_Click">Customer Care </asp:LinkButton></li>

                                <li><span>
                                    <img class="menuImg" src="Image/13.jpg" /></span>
                                    <asp:LinkButton ID="LinkButton56" runat="server" OnClick="lnkbtnAssets_Click"> Fixed Assets Management</asp:LinkButton></li>

                                <li><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span>
                                    <asp:LinkButton ID="LinkButton1" runat="server"> Legal and External Affairs  </asp:LinkButton></li>

                                <li><span>
                                    <img class="menuImg" src="Image/20.jpg" /></span>
                                    <asp:LinkButton ID="LinkButton9" runat="server" OnClick="lnkbtnMIS_Click"> MIS Module</asp:LinkButton></li>


                                <li><span>
                                    <img class="menuImg" src="Image/PF.png" /></span>
                                    <asp:LinkButton ID="LinkButton10" runat="server" OnClick="lnkbtnMM_Click"> Management Module</asp:LinkButton></li>


                                <li><span>
                                    <img class="menuImg" src="Image/MAR.png" /></span>
                                    <asp:LinkButton ID="LinkButton11" runat="server" OnClick="lnkbtnHR_Click"> Human Resource </asp:LinkButton></li>


                                <li><span>
                                    <img class="menuImg" src="Image/FIN.png" /></span>
                                    <asp:LinkButton ID="LinkButton12" runat="server" OnClick="lnkbtnKPI_Click"> Key Performance</asp:LinkButton></li>


                                <%--    <li><span>
                                    <img class="menuImg" src="Image/13.jpg" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=19")%>">Fixed Assets</a></li>

                                   <li><span>
                                    <img class="menuImg" src="Image/13.jpg" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=19")%>">Fixed Assets</a></li>

                                   <li><span>
                                    <img class="menuImg" src="Image/13.jpg" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=19")%>">Fixed Assets</a></li>

                                  <li><span>
                                    <img class="menuImg" src="Image/13.jpg" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=19")%>">Fixed Assets</a></li>

                               
                                <li><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=21")%>">MIS</a></li>--%>
                            </ul>

                        </div>
                        <div class="col-md-4">
                            <h3>Approvals</h3>
                              <%--<area shape="rect" coords="208,126,370,153" href="F_81_Hrm/F_82_App/EmpEntryForm.aspx" target="_self" />--%>
                           <ul>
                                <li><span>
                                    <img class="menuImg" src="Image/S2.png" /></span><a href="<%=this.ResolveUrl("~/F_99_Allinterface/SalesInterface.aspx")%>" target="_blank">Sales</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/FIN.png"  /></span><a href="<%=this.ResolveUrl("~/F_99_Allinterface/AccountInterface.aspx")%>" target="_blank">Accounts</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/OV.png" /></span><a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptPurInterface.aspx")%>" target="_blank">Purchase</a></li>
                      
                               
                              <%--  <li><span>
                                    <img class="menuImg" src="Image/procurment1.png" /></span><a href="<%=this.ResolveUrl("~/#")%>" target="_blank">Legal</a></li>--%>

                               <%-- <li><span>
                                    <img class="menuImg" src="Image/acc3.png" /></span><a href="<%=this.ResolveUrl("~/F_08_PPlan/ConstructionInfo.aspx")%>" target="_blank">Construction</a></li>--%>

                                <li><span>
                                    <img class="menuImg" src="Image/proj3.png" /></span><a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptEngInterface.aspx")%>" target="_blank">General Requisitions</a></li>


                                <li><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span><a href="<%=this.ResolveUrl("~/F_15_DPayReg/BillRegInterface.aspx")%>" target="_blank">Bill Register </a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/CONS2.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/InterfaceLeavApp.aspx")%>" target="_blank">Leave Interface</a></li>
                            </ul>

                            <%--     <ul class="dashCir">
                                <li><a href="<%=this.ResolveUrl("~/F_22_Sal/SalesInformation.aspx")%>"><span>
                                    <img class="" src="Image/S2.png" /></span>Sales</a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurInformation.aspx")%>"><span>
                                    <img class="" src="Image/procurment1.png" /></span>Procurement</a></li>

                                <li><a href="<%=this.ResolveUrl("F_08_PPlan/ConstructionInfo.aspx")%>"><span>
                                    <img class="" src="Image/CONS2.png" /></span>Construction</a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_18_MAcc/AccDashBoard.aspx")%>"><span>
                                    <img class="" src="Image/acc3.png" /></span>Accounts</a></li>
                               
                                 <li><a href="<%=this.ResolveUrl("~/F_32_Mis/RptMisMasterBgd.aspx?Type=InvPlan")%>"><span>
                                    <img class="" src="Image/proj3.png" /></span>Project</a></li>

                                 <li><a href="<%=this.ResolveUrl("~/F_46_GrMgtInter/RptGrpDailyReportJq.aspx")%>"><span>
                                    <img class="" src="Image/OV.png" /></span>Overall</a></li>
                          
                                 


                            </ul>--%>

                            <div class="clearfix"></div>
                        </div>
                        <div class="col-md-4">
                            <h3>Reports</h3>
                            <ul>
                                <li><span>
                                    <img class="menuImg" src="Image/LP.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=5000")%>">All Departmental Reports </a></li>
                              <%--  <li><span>
                                    <img class="menuImg" src="Image/PF.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_97_MIS/RptMgtInterface.aspx")%>">Summary</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/B1.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_99_MgtAct/RptgroupAttendance.aspx")%>">Attendance</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/B1.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/RptWeekPresence.aspx")%>">Attendance(W)</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/FIN.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Salary&Entry=Payroll")%>">Salary Sheet</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/PP.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RptSalarySummary.aspx?Type=SalSum")%>">Salary Sum</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/PP.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/EmpBankSalary.aspx?Type=Entry")%>">Forwarding</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/LP.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Payslip")%>">Payslip</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/CC.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/RptAttendenceSheet.aspx")%>">Attendance Information</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/CONS.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptEmpInformation.aspx?Type=EmpAllInfo")%>">Profile</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/MAR.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_84_Lea/RptEmpLeaveStatus02.aspx?Type=EmpLeaveStatus")%>">Leave Status (Dept)</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/MAR.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_85_Lon/EmpLoanStatus.aspx")%>">Loan</a></li>
                           
                                <li><span>
                                    <img class="menuImg" src="Image/S1.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=EmpCon")%>">Confirmation</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/CR.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=SepType")%>">Separation</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/CC.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_97_MIS/RptNewJoingInfo.aspx")%>">Joining</a></li>


                                <li><span>
                                    <img class="menuImg" src="Image/ABP.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptMyInterface.aspx?empid=")%>">My Interface</a></li>
--%>

                            </ul>
                            <%--<ul>
                               
                                <li><span>
                                    <img class="menuImg" src="Image/FLOW.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=8010")%>">Flow Chart</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/SE.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4110")%>">Settings</a></li>





                                <li><span>

                                    <img class="menuImg" src="Image/16.jpg" /></span>
                                    <asp:LinkButton ID="lnkbtnGeneral" runat="server" OnClick="lnkbtnGeneral_Click">General</asp:LinkButton>


                                </li>

                                <li><span>

                                    <img class="menuImg" src="Image/16.jpg" /></span>
                                    <asp:LinkButton ID="lnkbtnHr" runat="server" OnClick="lnkbtnHr_Click">HR Management</asp:LinkButton>


                                </li>


                                <li><span>

                                    <img class="menuImg" src="Image/16.jpg" /></span>
                                    <asp:LinkButton ID="lnkbtnKPI" runat="server" OnClick="lnkbtnKPI_Click">KPI</asp:LinkButton>


                                </li>




                                <li><span>
                                    <img class="menuImg" src="Image/GRP.png" /></span><a href="<%=this.ResolveUrl("~/")%>">Group Information</a></li>

                            </ul>--%>



                            <%--                                    <img class="menuImg" src="Image/16.jpg" />
                                    <asp:LinkButton ID="lnkbtnHr" runat="server" OnClick="lnkbtnHr_Click">HR Management</asp:LinkButton>
                            --%>
                        </div>
                    </div>

                    <div class="clearfix"></div>
                </asp:Panel>


                <asp:Panel runat="server" ID="PanelAllinsNew" Visible="false">
                    <style>
                        .comm{
                            font-weight:bold;
                            height:45px;
                            font-size:16px;
                            text-align:center;
                            margin:3px 0px;
                        }
                        .comm1{
                            font-weight:bold;
                            height:52px;
                            font-size:16px;
                            text-align:center;
                            margin:3px 0px;
                        }
                         span.btex{
                          color:#000;
                          line-height:35px;
                       }
                      
                       span.btex1{
                          color:#000;
                          line-height:45px;
                       }
                        .color01{
                            background-color:#D7E5BB;
                            border:2px solid #8EBECB;
                            
                        }
                          .color02{
                            background-color:#E6E0ED;
                            border:2px solid #769435;
                            
                        }
                           .color03{
                            background-color:#8CB3E5;
                            border:2px solid #769435;
                            
                        }
                          .color04{
                            background-color:#CCC1DB;
                            border:2px solid #96C2CE;
                            
                        }
                           .color05{
                            background-color:#F3DCDB;
                            border:2px solid #973632;
                            
                        }
                           .color06{
                            background-color:#B6DEE9;
                            border:2px solid #2B859D;
                        }
                           .color07{
                            background-color:#DEDAC2;
                            border:2px solid #2B859D;
                        }
                           .color08{
                            background-color:#DBEEF5;
                            border:2px solid #6E9FDE;
                        }
                           .color09{
                            background-color:#F3DCDB;
                            border:2px solid #973632;
                        }
                           .color010{
                            background-color:#E6E0ED;
                            border:2px solid #60487C;
                        }
                           .color011{
                            background-color:#D7E5BB;
                            border:2px solid #769435;
                        }
                           .color012{
                            background-color:#FEEBD9;
                            border:2px solid #E76C03;
                        }
                           .color013{
                            background-color:#E6E0ED;
                            border:2px solid #A394B4;
                        }
                    </style>
                    <div class="row flowMenu">
                        <div class="col-md-4">
                              <h3> OPERATIONS </h3>
                            <div class="row">
                             <div class="col-sm-12 col-md-12 col-lg-12">
                                <div class=" comm color01">
                                    <asp:LinkButton ID="lnkbtnStepOpra" OnClick="LinkButton8_Click" runat="server" >
                                        <span class='btex'>Land Procurement</span>
                                    </asp:LinkButton>
                                  </div>
                               </div>
                             <div class="col-sm-6 col-md-6 col-lg-6" style="margin-right:0px; padding-right:0px;">
                                <div class=" comm color01">
                                    <asp:LinkButton ID="LinkButton5"  OnClick="lnkbtnMatr_Click" runat="server" >
                                        <span class='btex'> Budgetary Control</span>
                                    </asp:LinkButton>
                                  </div>
                               </div>
                             <div class="col-sm-6 col-md-6 col-lg-6" style="margin-left:0px; padding-left:4px;">
                                <div class=" comm color01">
                                    <asp:LinkButton ID="LinkButton13" runat="server" OnClick="lnkbtnPlaning_Click">
                                        <span class='btex'>Project Planning </span>
                                    </asp:LinkButton>
                                  </div>
                               </div>

                             <div class="col-sm-12 col-md-12 col-lg-12">
                                <div class=" comm color02">
                                    <asp:LinkButton ID="LinkButton14" OnClick="btnImp_Click" runat="server" >
                                        <span class='btex'>Project Implementation </span>
                                    </asp:LinkButton>
                                  </div>
                               </div>
                             <div class="col-sm-6 col-md-6 col-lg-6" style="margin-right:0px; padding-right:0px;">
                                <div class=" comm color02">
                                    <asp:LinkButton ID="LinkButton15" OnClick="lnkbtnGoodsInv_Click" runat="server" >
                                        <span class='btex'> Inventory Control</span>
                                    </asp:LinkButton>
                                  </div>
                               </div>
                             <div class="col-sm-6 col-md-6 col-lg-6" style="margin-left:0px; padding-left:4px;">
                                <div class=" comm color02">
                                    <asp:LinkButton ID="LinkButton16" OnClick="btnPur_Click" runat="server" >
                                        <span class='btex'> Procurement Module </span>
                                    </asp:LinkButton>
                                  </div>
                               </div>
                           
                             <div class="col-sm-12 col-md-12 col-lg-12">
                                <div class=" comm color03">
                                    <asp:LinkButton ID="LinkButton17" OnClick="lnkbtnACC_Click" runat="server" >
                                        <span class='btex'> General Accounts </span>
                                    </asp:LinkButton>
                                  </div>
                               </div>
                             <div class="col-sm-6 col-md-6 col-lg-6" style="margin-right:0px; padding-right:0px;">
                                <div class=" comm color03">
                                    <asp:LinkButton ID="LinkButton18" OnClick="lnkbtnSale_Click" runat="server" >
                                        <span class='btex'> Sales </span>
                                    </asp:LinkButton>
                                  </div>
                               </div>
                             <div class="col-sm-6 col-md-6 col-lg-6" style="margin-left:0px; padding-left:4px;">
                                <div class=" comm color03">
                                    <asp:LinkButton ID="LinkButton19" OnClick="lnkbtnCR_Click" runat="server" >
                                        <span class='btex'> Credit Realization </span>
                                    </asp:LinkButton>
                                  </div>
                               </div>

                             <div class="col-sm-12 col-md-12 col-lg-12">
                                <div class=" comm color04">
                                    <asp:LinkButton ID="LinkButton20" OnClick="lnkbtnAssets_Click" runat="server" >
                                        <span class='btex'> Fixed Asset Management </span>
                                    </asp:LinkButton>
                                  </div>
                               </div>
                             <div class="col-sm-6 col-md-6 col-lg-6" style="margin-right:0px; padding-right:0px;">
                                <div class=" comm color04">
                                    <asp:LinkButton ID="LinkButton21" OnClick="lnkbtnMIS_Click" runat="server" >
                                        <span class='btex'> MIS Module </span>
                                    </asp:LinkButton>
                                  </div>
                               </div>
                             <div class="col-sm-6 col-md-6 col-lg-6" style="margin-left:0px; padding-left:4px;">
                                <div class=" comm color04">
                                    <asp:LinkButton ID="LinkButton22" OnClick="lnkbtnMM_Click" runat="server" >
                                        <span class='btex'> Management Module </span>
                                    </asp:LinkButton>
                                  </div>
                               </div>

                             <div class="col-sm-12 col-md-12 col-lg-12">
                                <div class=" comm color05">
                                    <asp:LinkButton ID="LinkButton23"  OnClick="lnkbtnHR_Click" runat="server" >
                                        <span class='btex'> Human Resource & Administrator </span>
                                    </asp:LinkButton>
                                  </div>
                               </div>

                           </div>             
                        </div>
                        <div class="col-md-4">
                            <h3> APPROVALS</h3>

                              <div class="row">
                              <div class="col-sm-12 col-md-12 col-lg-12">
                                <div class=" comm1 color06">
                                    <asp:LinkButton ID="LinkButton24"  PostBackUrl="F_99_Allinterface/RptPurInterface.aspx?Type=Report&comcod=" runat="server" >
                                        <span class='btex1'> Purchase Requisition</span>
                                    </asp:LinkButton>
                                  </div>
                               </div>
                              <div class="col-sm-12 col-md-12 col-lg-12">
                                <div class=" comm1 color06">
                                    <asp:LinkButton ID="LinkButton25" PostBackUrl="F_99_Allinterface/SalesInterface.aspx?Type=Report&comcod=" runat="server" >
                                        <span class='btex1'>Sales Interface</span>
                                    </asp:LinkButton>
                                  </div>
                               </div>
                              <div class="col-sm-6 col-md-6 col-lg-6" style="margin-right:0px; padding-right:0px">
                                <div class=" comm1 color06">
                                    <asp:LinkButton ID="LinkButton26" PostBackUrl="F_99_Allinterface/AccountInterface.aspx?Type=Report&comcod=" runat="server" >
                                        <span class='btex1'>Accounts Interface</span>
                                    </asp:LinkButton>
                                  </div>
                               </div>

                         <div class="col-sm-6 col-md-6 col-lg-6">
                                <div class=" comm1 color06">
                                    <a href="<%=this.ResolveUrl("~/F_99_Allinterface/SubContractorBillInterface.aspx?Type=Report&comcod=")%>"> <span class='btex1'>Sub-Contractor Interface</span></a>
                                
                                  </div>
                               </div>

                              <div class="col-sm-12 col-md-12 col-lg-12">
                                <div class=" comm1 color06">
                                    <asp:LinkButton ID="LinkButton27" PostBackUrl="F_99_Allinterface/RptEngInterface.aspx" runat="server" >
                                        <span class='btex1'>General Requisition</span>
                                    </asp:LinkButton>
                                  </div>
                               </div>
                              <div class="col-sm-12 col-md-12 col-lg-12">
                                <div class=" comm1 color06">
                                    <asp:LinkButton ID="LinkButton28" PostBackUrl="F_15_DPayReg/BillRegInterface.aspx?Type=Report&comcod=" runat="server" >
                                        <span class='btex1'>Bill  Register</span>
                                    </asp:LinkButton>
                                  </div>
                               </div>
                              <div class="col-sm-12 col-md-12 col-lg-12">
                                <div class=" comm1 color06">
                                    <asp:LinkButton ID="LinkButton29" PostBackUrl="F_81_Hrm/F_92_Mgt/InterfaceLeavApp.aspx?Type=Mgt" runat="server" >
                                        <span class='btex1'>Leave Interface</span>
                                    </asp:LinkButton>
                                  </div>
                               </div>
                              <div class="col-sm-12 col-md-12 col-lg-12">
                                <div class=" comm1 color06">
                                    <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_84_Lea/MyLeave.aspx?Type=User")%>"> <span class='btex1'> Online Leave Application</span></a>
                                  <%--  <asp:LinkButton ID="LinkButton30" PostBackUrl="F_81_Hrm/F_84_Lea/HREmpLeave.aspx?Type=LeaveApp" runat="server" >
                                        <span class='btex1'>Online Leave Application</span>
                                    </asp:LinkButton>--%>
                                  </div>
                               </div>
                              <div class="col-sm-12 col-md-12 col-lg-12">
                                <div class=" comm1 color06">
                                     <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/HRDailyAtten.aspx")%>"> <span class='btex1'> Daily Attendance System </span></a>
                                   
                                 <%--   <asp:LinkButton ID="LinkButton31"  PostBackUrl="F_81_Hrm/F_83_Att/HRDailyAttenManually.aspx" runat="server" >
                                        <span class='btex1'>Daily Attendance System </span>
                                    </asp:LinkButton>--%>
                                  </div>
                               </div>
                              </div>

                            <div class="clearfix"></div>
                        </div>
                        <div class="col-md-4">
                            <h3> REPORTS</h3>

                                  <div class="row">
                              <div class="col-sm-12 col-md-12 col-lg-12">
                                <div class=" comm1 color07">
                                     <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_99_MgtAct/RptgroupAttendance?Type=Dept")%>"> <span class='btex1'> Daily Attendance </span></a>
                                   <%-- <asp:LinkButton ID="LinkButton32" PostBackUrl="F_81_Hrm/F_83_Att/RptWeekPresence.aspx" runat="server" >
                                        <span class='btex1'> Daily Attendance </span>
                                    </asp:LinkButton>--%>
                                  </div>
                               </div>
                              <div class="col-sm-12 col-md-12 col-lg-12">
                                <div class=" comm1 color08">
                                     <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RptSalSummary02.aspx?Type=SalSummary")%>"> <span class='btex1'>Salary Summary </span></a>
                                   
                                  </div>
                               </div>
                              <div class="col-sm-12 col-md-12 col-lg-12">
                                <div class=" comm1 color09">
                                     <a href="<%=this.ResolveUrl("~/F_32_Mis/RptMisMasterBgd.aspx?Type=InvPlan&comcod=")%>"> <span class='btex1'> Project Status Report</span></a>
                                <%--    <asp:LinkButton ID="LinkButton34" PostBackUrl="F_17_Acc/RptAccCollVsClearance.aspx?Type=DailyPayment" runat="server" >
                                        <span class='btex1'>Project Status Report</span>
                                    </asp:LinkButton>--%>
                                  </div>
                               </div>
                              <div class="col-sm-6 col-md-6 col-lg-6"style="margin-right:0px; padding-right:0px">
                                <div class=" comm1 color06">
                                    <a href="<%=this.ResolveUrl("~/F_17_Acc/AccFinalReports.aspx?RepType=IS&comcod=")%>"> <span class='btex1'> Income Statement </span></a>
                                
                                  </div>
                               </div>

                                         <div class="col-sm-6 col-md-6 col-lg-6">
                                <div class=" comm1 color06">
                                    <a href="<%=this.ResolveUrl("~/F_17_Acc/AccFinalReports.aspx?RepType=BS&comcod=")%>"> <span class='btex1'> Balance Sheet </span></a>
                                
                                  </div>
                               </div>

                              <div class="col-sm-12 col-md-12 col-lg-12">
                                <div class=" comm1 color010">
                                    <asp:LinkButton ID="LinkButton36" PostBackUrl="F_17_Acc/AccTrialBalance.aspx?Type=Mains&comcod=" runat="server" >
                                        <span class='btex1'>Trial Balance </span>
                                    </asp:LinkButton>
                                  </div>
                               </div>
                              <div class="col-sm-12 col-md-12 col-lg-12">
                                <div class=" comm1 color011">
                                    <asp:LinkButton ID="LinkButton37" PostBackUrl="F_17_Acc/RptBankCheque.aspx?Type=CashFlow" runat="server" >
                                        <span class='btex1'>Statement Of Cash Flow </span>
                                    </asp:LinkButton>
                                  </div>
                               </div>
                              <div class="col-sm-6 col-md-6 col-lg-6" style="margin-right:0px; padding-right:0px">
                                <div class=" comm1 color012">
                                     <a href="<%=this.ResolveUrl("~/F_17_Acc/RptAccSpLedger.aspx?Type=ASupConPayment&comcod=")%>"> <span class='btex1'>Accounts Payable </span></a>
                             
                                  </div>
                               </div> 
                                <div class="col-sm-6 col-md-6 col-lg-6">
                                <div class=" comm1 color012">
                                     <a href="<%=this.ResolveUrl("~/F_23_CR/RptReceivedList04.aspx?Type=AllProDuesCollect&comcod=")%>"> <span class='btex1'>Accounts Receivable </span></a>
                             
                                  </div>
                               </div> 
                              <div class="col-sm-12 col-md-12 col-lg-12">
                                <div class=" comm1 color013">
                                        <a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=5000")%>"> <span class='btex1'>General Reports </span></a>
                                  </div>
                               </div>
                              </div>

                            
                        
                        </div>
                    </div>

                    <div class="clearfix"></div>
                </asp:Panel>

               

             

                <asp:Panel runat="server" ID="PanelHR" Visible="false">
                    <div class="row flowMenu">
                        <div class="col-md-1"></div>
                        <div class="col-md-3">
                            <h3 style="padding: 0 0px; text-align: center"><span class="pull-left"></span>Modules</h3>
                            <ul>
                                <li><span>
                                    <img class="menuImg" src="Image/rec.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Recruitment</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/app.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Appointment</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/att.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Attendance</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/lea.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Leave</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/loan.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Loan</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/allo.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Allowance</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/trns.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Transfer</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/reg.png" /></span><a href="<%=this.ResolveUrl("~/")%>">Separation</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/pay.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Payroll</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/pf.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">P.F Account</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/acr.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">ACR</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/inc.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Increment</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/mgmt.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Management</a></li>




                            </ul>

                        </div>
                        <div class="col-md-4">
                            <h3>DashBoard</h3>
                            <ul class="dashCir">
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/AllEmpList.aspx")%>"><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span>Summary</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/RptEmpDailyAttendance.aspx?Type=DailyAtten")%>"><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span>Attendance</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptEmpInformation.aspx?Type=Services")%>"><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span>History</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=joiningRpt")%>"><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span>Joining</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=EmpCon")%>"><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span>Provision</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=SepType")%>"><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span>Separation</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Salary&Entry=Payroll")%>"><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span>Salary</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Salary&Entry=Payroll")%>"><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span>ACR</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptMyInterface.aspx")%>"><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span>My Interface</a></li>
                            </ul>
                        </div>
                        <div class="col-md-3">
                            <h3>Action</h3>
                            <ul>
                                <li><span>
                                    <img class="menuImg" src="Image/FLOW.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4101")%>">Flow Chart</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/ALL.png" /></span><a href="GenPage.aspx?Type=All">All Reports</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/SE.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4110")%>">Settings</a></li>

                            </ul>
                        </div>
                    </div>

                    <div class="clearfix"></div>
                </asp:Panel>

                


            </div>
        </div>

        <asp:Panel runat="server" ID="PnlGrp" Visible="false">
            <div class="row flowMenu">
                <div class="col-md-1"></div>
                
                <div class="col-md-4">
                    <h3 style="text-align: left;padding-left: 12px">List Of Companies</h3>
                    <asp:DropDownList ID="ddlComName" class="ComName form-control ClCompAndMod" runat="server" TabIndex="2" onchange="navFromList(this.value);">
                       
                    </asp:DropDownList>
                    <%--<ul id="leftul" class="hover-item"  clientidmode="Static">
                    </ul>--%>
                    <div class="clearfix"></div>
                </div>
                <div class="col-md-3 pading5px">
                    <h3 style="text-align: left;padding-left: 12px">Group Reports</h3>
                    <ul>
                        <li><span>
                            <img class="menuImg" src="Image/B1.png" /></span><a href="<%=this.ResolveUrl("~/F_45_GrAcc/RptAccRecPayment.aspx?Type=RecAndPayment")%>" target="_blank">Receipts & Payment</a></li>

                        <%--<li> <span>
                            <img class="menuImg" src="Image/B1.png" /></span><a href="<%=this.ResolveUrl("~/F_45_GrAcc/RptAccRecPayment.aspx?Type=RecAndPayment01")%>" target="_blank">Receipts & Payment 1 </a></li>--%>
                        <li><span>
                            <img class="menuImg" src="Image/PF.png" /></span><a href="<%=this.ResolveUrl("~/F_45_GrAcc/RptGrpAccDailyTransaction.aspx?Type=GrpDTransaction")%>" target="_blank">Daily Transaction</a></li>
                        <li><span>
                            <img class="menuImg" src="Image/LP.png" /></span><a href="<%=this.ResolveUrl("~/F_45_GrAcc/RptAccRecPayment.aspx?Type=BankBalance")%>" target="_blank">Bank Balance</a></li>
                        <li><span>
                            <img class="menuImg" src="Image/PP.png" /></span><a href="<%=this.ResolveUrl("~/F_45_GrAcc/RptAccRecPayment.aspx?Type=Schedule")%>" target="_blank">Schedule</a></li>
                        <li><span>
                            <img class="menuImg" src="Image/CONS.png" /></span><a href="<%=this.ResolveUrl("~/F_45_GrAcc/RptAccRecPayment.aspx?Type=TrialBalance")%>" target="_blank">Trial Balance</a></li>
                        <li><span>
                            <img class="menuImg" src="Image/MS.png" /></span><a href="<%=this.ResolveUrl("~/F_45_GrAcc/RptAccRecPayment.aspx?Type=IncomeStatement")%>" target="_blank">Statement of Comprehensive Income</a></li>
                        <li><span>
                            <img class="menuImg" src="Image/REG.png" /></span><a href="<%=this.ResolveUrl("~/F_45_GrAcc/RptAccRecPayment.aspx?Type=BalanceSheet")%>" target="_blank">Statement of Financial Position</a></li>
                        <li><span>
                            <img class="menuImg" src="Image/FIN.png" /></span><a href="<%=this.ResolveUrl("~/F_45_GrAcc/RptAccRecPayment.aspx?Type=BalanceDet")%>" target="_blank">Bank Balance Details</a></li>
                        <li><span>
                            <img class="menuImg" src="Image/006.png" /></span><a href="<%=this.ResolveUrl("~/F_45_GrAcc/RptAccRecPayment.aspx?Type=IssueVsCollect")%>" target="_blank">Issue Vs. Collection</a></li>
                        
                        <li><span>
                            <img class="menuImg" src="Image/SO.png" /></span><a href="<%=this.ResolveUrl("~/F_45_GrAcc/RptAccRecPayment.aspx?Type=CashFlow")%>" target="_blank">Statement of Cash Flow</a></li>
                        <li><span>
                            <img class="menuImg" src="Image/ACC.png" /></span><a href="<%=this.ResolveUrl("~/F_45_GrAcc/RptGrpAccDailyTransaction.aspx?Type=GrpWBudVsAchv")%>" target="_blank">Working Vs Acheivement</a></li>

                    </ul>

                </div>
                <div class="col-md-4">
                            <h3 style="text-align: left;padding-left: 12px">HR Reports</h3>
                            <ul>
                                <%-- <li><span>
                                    <img class="menuImg" src="Image/LP.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/AllEmpList.aspx")%>">Members</a></li>--%>
                                
                                <li>
                                    <span><img class="menuImg" src="Image/LP.png"/></span>
                                    <asp:HyperLink runat="server" Target="_blank" CssClass="hrreport" id="hlmember" ClientIDMode="Static">Member</asp:HyperLink>
                                </li>
                                <li><span>
                                    <img class="menuImg" src="Image/PF.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_97_MIS/RptMgtInterface.aspx")%>" target="_blank">Summary</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/B1.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_99_MgtAct/RptgroupAttendance?Type=Dept")%>" target="_blank">Attendance</a></li>

                                <%--<li><span>
                                    <img class="menuImg" src="Image/B1.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/RptWeekPresence.aspx")%>">Attendance(W)</a></li>--%>
                                <%--<li><span>
                                    <img class="menuImg" src="Image/FIN.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Salary&Entry=Payroll")%>">Salary Sheet</a></li>--%>

                                <li><span>
                                    <img class="menuImg" src="Image/PP.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RptSalarySummary.aspx?Type=SalSum")%>" target="_blank">Salary Sum</a></li>
                                <%--<li><span>
                                    <img class="menuImg" src="Image/PP.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/EmpBankSalary.aspx?Type=Entry")%>">Forwarding</a></li>--%>
                                <%--<li><span>
                                    <img class="menuImg" src="Image/LP.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Payslip")%>">Payslip</a></li>--%>
                                <%--<li><span>
                                    <img class="menuImg" src="Image/CC.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/RptAttendenceSheet.aspx")%>">Attendance Information</a></li>--%>
                                <li><span>
                                    <img class="menuImg" src="Image/CONS.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptEmpInformation.aspx?Type=EmpAllInfo")%>" target="_blank">Profile</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/MAR.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_84_Lea/RptEmpLeaveStatus02.aspx?Type=EmpLeaveStatus")%>" target="_blank">Leave Status (Dept)</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/MAR.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_85_Lon/EmpLoanStatus.aspx")%>" target="_blank">Loan</a></li>
                                <%-- <li><span>
                                    <img class="menuImg" src="Image/MS.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=mspanal")%>">Materials Store</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/MAR.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=Marketpanal")%>">Marketing</a></li>--%>
                                <li><span>
                                    <img class="menuImg" src="Image/S1.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=EmpCon")%>" target="_blank">Confirmation</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/CR.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=SepType")%>" target="_blank">Separation</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/CC.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_97_MIS/RptNewJoingInfo.aspx")%>" target="_blank">Joining</a></li>


                                <%-- %> <li><span>
                                    <img class="menuImg" src="Image/ACC.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Salary&Entry=Payroll")%>">ACR</a></li>--%>

                                <%--<li><span>
                                    <img class="menuImg" src="Image/ABP.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptMyInterface.aspx?empid=")%>">My Interface</a></li>--%>


                            </ul>
                            <%--<ul>
                               
                                <li><span>
                                    <img class="menuImg" src="Image/FLOW.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=8010")%>">Flow Chart</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/SE.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4110")%>">Settings</a></li>





                                <li><span>

                                    <img class="menuImg" src="Image/16.jpg" /></span>
                                    <asp:LinkButton ID="lnkbtnGeneral" runat="server" OnClick="lnkbtnGeneral_Click">General</asp:LinkButton>


                                </li>

                                <li><span>

                                    <img class="menuImg" src="Image/16.jpg" /></span>
                                    <asp:LinkButton ID="lnkbtnHr" runat="server" OnClick="lnkbtnHr_Click">HR Management</asp:LinkButton>


                                </li>


                                <li><span>

                                    <img class="menuImg" src="Image/16.jpg" /></span>
                                    <asp:LinkButton ID="lnkbtnKPI" runat="server" OnClick="lnkbtnKPI_Click">KPI</asp:LinkButton>


                                </li>




                                <li><span>
                                    <img class="menuImg" src="Image/GRP.png" /></span><a href="<%=this.ResolveUrl("~/")%>">Group Information</a></li>

                            </ul>--%>



                            <%--                                    <img class="menuImg" src="Image/16.jpg" />
                                    <asp:LinkButton ID="lnkbtnHr" runat="server" OnClick="lnkbtnHr_Click">HR Management</asp:LinkButton>
                            --%>
                        </div>


                <%--<div class="col-md-3 pading5px">
                    <h3>Group Reports</h3>

                    <ul class="dashCir">
                        <li><a href="<%=this.ResolveUrl("~/F_46_GrMgtInter/RptGrpDailyReportJq.aspx")%>"><span>
                            <img class="" src="Image/FLOW.png" /></span>Operation Monitoring</a></li>
                        <li></li>
                        <li><a href="<%=this.ResolveUrl("~/F_45_GrAcc/RptGrpMisDailyActiviteisJq.aspx")%>"><span>
                            <img class="" src="Image/SE.png" /></span>Interface(Overall)</a></li>

                    </ul>

                </div>--%>
            </div>

            <div class="clearfix"></div>
        </asp:Panel>
        <asp:Panel runat="server" ID="PnlGrpDetails" Visible="false">
            <div class="row flowMenu">
                <div class="col-md-1"></div>
                <div class="col-md-3 pading5px">
                </div>


                <div class="col-md-4">
                    <h3 style="padding-left: 118px;">DashBoard</h3>
                    <ul class="dashCir">
                        <li>
                            <asp:LinkButton ID="btnSales" runat="server" OnClick="btnSales_Click"><span>
                                    <img class="" src="Image/S2.png" /></span>Sales</asp:LinkButton></li>
                        <li>
                            <asp:LinkButton ID="btnPur" runat="server" OnClick="btnPur_Click"><span>
                                    <img class="" src="Image/P.png" /></span>Purchase</asp:LinkButton></li>

                        <li>
                            <asp:LinkButton ID="btnAcc" runat="server" OnClick="btnAcc_Click"><span>
                                    <img class="" src="Image/ACC2.png" /></span>Accounts</asp:LinkButton></li>
                        <li>
                            <asp:LinkButton ID="btnPro" runat="server" OnClick="btnPro_Click"><span>
                                    <img class="" src="Image/PRO.png" /></span>Construction</asp:LinkButton></li>

                        <li></li>
                        <li>
                            <asp:LinkButton ID="btnOver" runat="server" OnClick="btnOver_Click"><span>
                                    <img class="" src="Image/15.png" /></span>Overall</asp:LinkButton></li>


                   

                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="col-md-3 pading5px">
                </div>
            </div>

            <div class="clearfix"></div>
        </asp:Panel>
        <asp:Panel ID="pnlAdminPermission" runat="server" Visible="False">
            <div class="lbl2SubMenu headTagh3">
                <div class="contentPart">
                    <div class="row">
                        <div class="col-md-4"></div>
                        <div class="col-md-4 adminpermission">

                            <ul class="nav colRight " id="Rightul">
                                <li>
                                    <h3>Admin Permission</h3>
                                </li>
                                <li><a href="F_34_Mgt/UserLoginfrm.aspx">01. User Permission</a></li>
                                <li><a href="F_14_Pro/ProjectLink.aspx">02. Project Permission</a></li>
                                <li><a href="F_34_Mgt/AccUserCash.aspx">03. Cash &amp; Bank Permission</a></li>
                                <li><a href="F_34_Mgt/Tranlimitdate.aspx">04. Transaction Limit Day</a></li>
                                <li><a href="F_34_Mgt/UserImage.aspx">05. User Image Upload</a></li>
                                <li><a href="F_34_Mgt/RptUserLogDetails.aspx">06. Entry, Edit &amp; Cancellation Record</a></li>
                                <li><a href="F_34_Mgt/RptUserLogStatus.aspx">07. User Log Information</a></li>
                                <li><a href="F_34_Mgt/DataBackup.aspx">08. Auto Data Backup</a></li>
                                <li><a href="F_17_Acc/AccCodeBook.aspx?InputType=Accounts">09. Accounts Code Book</a></li>
                                <li><a href="F_17_Acc/AccSubCodeBook.aspx?InputType=ResCodePrint">10. Details Code Book</a></li>
                                <li><a href="F_17_Acc/AccOpening.aspx">11. Accounts Opening</a></li>
                            </ul>


                        </div>
                        <div class="col-md-4"></div>
                    </div>
                </div>

            </div>
        </asp:Panel>



        <asp:Panel runat="server" Visible="false" ID="pnlkMIS">
            <style>
                .companysummary {
                    height: 40px;
                    left: 295px;
                    position: absolute;
                    text-align: center !important;
                    top: 90px;
                    width: 40px;
                }

                .na_Entry {
                    height: 40px;
                    left: 195px;
                    position: absolute;
                    text-align: center !important;
                    top: 90px;
                    width: 40px;
                }

                .na_MyInterface {
                    height: 40px;
                    left: 395px;
                    position: absolute;
                    text-align: center;
                    top: 90px;
                    width: 40px;
                }

                .Other {
                    height: 40px;
                    left: 495px;
                    position: absolute;
                    text-align: center;
                    top: 90px;
                    width: 40px;
                }

                /*.Other {
                            height: 40px;
                            left: 595px;
                            position: absolute;
                            text-align: center;
                            top: 90px;
                            width: 40px;
                        }*/

                .na_Sales {
                    height: 40px;
                    left: 195px;
                    position: absolute;
                    text-align: center;
                    top: 200px;
                    width: 40px;
                }

                .na_General {
                    height: 40px;
                    left: 295px;
                    position: absolute;
                    text-align: center;
                    top: 200px;
                    width: 40px;
                }

                .CR {
                    height: 40px;
                    left: 395px;
                    position: absolute;
                    text-align: center;
                    top: 200px;
                    width: 40px;
                }

                .Legal {
                    height: 40px;
                    left: 495px;
                    position: absolute;
                    text-align: center;
                    top: 200px;
                    width: 40px;
                }

                .na_JobSetup {
                    height: 40px;
                    left: 595px;
                    position: absolute;
                    text-align: center;
                    top: 200px;
                    width: 40px;
                }

                .deshboard {
                    width: 70px;
                }

                    .deshboard a img {
                        display: table;
                        margin: 0 auto;
                    }

                    .deshboard a.span {
                        display: block;
                        text-align: center;
                    }

                    .deshboard span.inttext {
                        color: #000;
                        display: block;
                        text-align: center;
                    }

                .dashCir {
                    border: 1px solid #85e11e;
                    border-radius: 50%;
                    display: block;
                    height: 45px;
                    margin: 0 auto;
                    padding: 2px;
                    width: 45px;
                }
            </style>

            <div class="row ">

                <div class="col-md-3 mfgacc ">
                    <ul class="sideMenu ">

                        <li>
                            <img src="Image/int11.png" />
                            <span style="padding: 0px; margin-left: 12px;">Interface</span></li>
                        <br>
                        <br>

                        <li><a href="F_15_Acc/RptAccDTransaction.aspx?Type=Accounts&amp;TrMod=DTran">Status Report</a></li>

                        <li><a href='<%=this.ResolveUrl("~/GenPage.aspx?Type=09")%>'>Monthly Report </a></li>
                        <li><a href="DeafultMenu.aspx?Type=4110">More Reports</a></li>

                        <li><a href="#">Login By:</a></li>
                        <li><a href="#">Computer:</a></li>
                    </ul>

                </div>

                <div class="col-md-9">


                    <div class="row menu10">
                        <div class="companysummary  deshboard">
                            <a href="<%=this.ResolveUrl("~/F_39_MyPage/RptDeptEvaSheet.aspx?Type=DeptTarVAch")%>" target="_self">
                                <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                <span class="inttext">Summary</span>
                            </a>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </div>


                        <div class="na_MyInterface  deshboard">
                            <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptMyInterface.aspx")%>" target="_self">
                                <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                <span class="inttext">My Interface</span>
                            </a>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </div>

                        <div>
                            <div class="na_Sales deshboard">
                                <a href="<%=this.ResolveUrl("~/F_47_Kpi/RptEmpEvaSheet.aspx?Type=Mgt")%>" target="_self">
                                    <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                    <span class="inttext">Sales</span>
                                </a>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </div>


                            <div class="Other deshboard">
                                <a href="<%=this.ResolveUrl("~/")%>" target="_self">
                                    <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                    <span class="inttext">Others</span>
                                </a>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </div>


                            <div class="na_General deshboard">
                                <a href="<%=this.ResolveUrl("~/F_39_MyPage/RptEmpEvaSheetGen.aspx?Type=Mgt")%>" target="_self">
                                    <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                    <span class="inttext">General</span>
                                </a>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </div>

                            <%--  <div class="na_Individual deshboard">
                                        <a href="<%=this.ResolveUrl("~/F_39_MyPage/EmpKpiEntry04All.aspx?Type=Mgt")%>" target="_self">
                                            <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                            <span class="inttext">Entry</span>
                                        </a>

                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </div>--%>


                            <div class="na_MyInterface  deshboard">
                                <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptMyInterface.aspx")%>" target="_self">
                                    <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                    <span class="inttext">My Interface</span>
                                </a>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </div>





                            <%--  <div class="na_JobList deshboard">
                                        <a href="<%=this.ResolveUrl("~/F_34_Mgt/ActivitiesCode.aspx?Type=DeptList")%>" target="_self">
                                            <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                            <span class="inttext">Job List</span>
                                        </a>
                                    </div>--%>

                            <%-- <div class="na_JobSetup deshboard">
                                        <a href="<%=this.ResolveUrl("~/F_34_Mgt/DeptWiseEmpList.aspx")%>" target="_self">
                                            <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                            <span class="inttext">Job Set-up</span>
                                        </a>
                                    </div>--%>



                            <div class="CR deshboard">
                                <a href="<%=this.ResolveUrl("~/")%>" target="_self">
                                    <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                    <span class="inttext">CR</span>
                                </a>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </div>

                        </div>
                    </div>




                    <div class="Legal deshboard">
                        <a href="<%=this.ResolveUrl("~/")%>" target="_self">
                            <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                            <span class="inttext">Legal</span>
                        </a>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </div>



                    <%--<div class="na_WeightSetup deshboard">
                                        <a href="<%=this.ResolveUrl("~/F_21_Kpi/EmpStdKpi.aspx")%>" target="_self">
                                            <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                            <span class="inttext">Weight Set-up</span>
                                        </a>
                                    </div>--%>





                    <div class="na_Entry deshboard">
                        <a href="<%=this.ResolveUrl("~/F_39_MyPage/EmpKpiEntry04All.aspx?Type=Mgt")%>" target="_self">
                            <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                            <span class="inttext">Entry</span>
                        </a>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </div>


                    <%--    <div class="Other deshboard">
                                <a href="<%=this.ResolveUrl("~/")%>" target="_self">
                                    <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                    <span class="inttext">Others</span>
                                </a>
                            </div>--%>
                </div>





                <div class="row">

                    <div class="col-md-12 relatedItems" style="width: 80%; float: right;">
                        <h3>Related Items</h3>
                        <ul class="nav-pills">
                            <%--  <li><a href="<%=this.ResolveUrl("~/F_34_Mgt/ActivitiesCode.aspx?Type=DeptList")%>"><span class=""></span>Work List </a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_34_Mgt/EntryDeptLink.aspx")%>"><span class=""></span>Dep. Link </a></li>--%>
                            <li><a href="<%=this.ResolveUrl("~/F_64_Mgt/DeptWiseEmpList.aspx")%>"><span class=""></span>Setup(General)</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_47_Kpi/EmpStdKpi.aspx")%>"><span class=""></span>Setup(Sales) </a></li>
                            <%--  <li><a href="<%=this.ResolveUrl("~/F_39_MyPage/EmpKpiEntry04All.aspx?Type=Mgt")%>"><span class=""></span>Entry </a></li>--%>
                        </ul>

                    </div>
                </div>

                <div class="clearfix"></div>
            </div>


            <div class="clearfix"></div>
        </asp:Panel>



        <div class="row">
            <asp:Panel ID="pnlflochart" runat="server" CssClass="pnlflowchart" Visible="false">

                <style>
                    * {
                        margin: 0;
                        padding: 0;
                    }

                    .pnlflowchart {
                        overflow: hidden;
                        background-image: url(Image/bg.PNG) !important;
                    }

                    #na_wrawpr {
                        width: 1100px;
                        height: auto;
                        margin: 0 auto;
                        position: relative;
                        background-image: url(image/bg.PNG);
                        /*background-color: #F7F7F7;
                            background-image: linear-gradient(90deg, rgba(255,255,255,.07) 50%, transparent 50%),
                            linear-gradient(90deg, rgba(255,255,255,.13) 50%, transparent 50%),
                            linear-gradient(90deg, transparent 50%, rgba(255,255,255,.17) 50%),
                            linear-gradient(90deg, transparent 50%, rgba(255,255,255,.19) 50%);
                            background-size: 13px, 29px, 37px, 53px;*/
                    }

                    .na_first {
                        width: 160px;
                        float: left;
                    }

                    #na_wrawpr ul {
                        margin: 0;
                        padding: 0;
                    }

                        #na_wrawpr ul li {
                            list-style: none;
                            margin: 1px 0;
                            text-align: center;
                            height: 30px;
                        }

                    .na_first ul li h5 {
                        background: #f0fee6 none repeat scroll 0 0 !important;
                        border: 1px solid #699f44;
                        box-shadow: 0 0 4px 2px #bec9b6 inset;
                        font-family: "ar_cenaregular";
                        font-size: 17px;
                        font-weight: normal;
                        line-height: 28px;
                        text-align: center;
                        vertical-align: middle;
                        margin: 0;
                        color: #000;
                        padding: 0;
                    }

                    .na_first ul li a.MMenu {
                        background: #f0fee6 none repeat scroll 0 0 !important;
                        border: 1px solid #699f44;
                        box-shadow: 0 0 4px 2px #bec9b6 inset;
                        font-family: "ar_cenaregular";
                        font-size: 17px;
                        font-weight: normal;
                        line-height: 28px;
                        text-align: center;
                        vertical-align: middle;
                        margin: 0;
                        color: #000;
                        padding: 0;
                    }

                    .na_first ul li a {
                        background: rgba(0, 0, 0, 0) linear-gradient(to bottom, #ffffff 0%, #fbfbfb 49%, #d2d2d2 98%, #cce0f2 100%) repeat scroll 0 0;
                        font-family: "ar_cenaregular";
                        font-size: 14px;
                        font-weight: normal;
                        line-height: 28px;
                        display: block;
                        color: #000;
                        text-align: center;
                        vertical-align: middle;
                        border: 1px solid #999;
                        margin: 0;
                        padding: 0;
                        text-decoration: none;
                    }

                    .na_first2 {
                        width: 75px;
                        float: left;
                    }

                        .na_first2 ul li span {
                            height: 30px;
                            font-size: 20px;
                            color: #903;
                        }

                        .na_first2 ul li i {
                            height: 30px;
                            font-size: 20px;
                            color: #903;
                        }

                    .na_first ul li span {
                        height: 30px;
                        font-size: 20px;
                        color: green;
                    }

                    .na_first ul li i {
                        height: 30px;
                        font-size: 20px;
                        color: #903;
                    }

                    [class^="flaticon-"]::before, [class*=" flaticon-"]::before, [class^="flaticon-"]::after, [class*=" flaticon-"]::after {
                        font-size: 20px !important;
                        margin: 0;
                        padding: 0 10px 0 2px !important;
                        width: 20px !important;
                    }


                    .na_first ul a {
                        text-align: left !important;
                    }

                    .na_first ul h5 {
                        text-align: left !important;
                    }

                    .na_linqC1a {
                        background: green;
                        height: 50px;
                        left: 78px;
                        position: absolute;
                        top: 160px;
                        width: 3px;
                    }

                    .na_linqC2a {
                        background: #903 none repeat scroll 0 0;
                        height: 98px;
                        left: 209px;
                        position: absolute;
                        top: 170px;
                        width: 3px;
                    }

                    .na_linqC20a {
                        background: green;
                        height: 50px;
                        left: 78px;
                        position: absolute;
                        top: 282px;
                        width: 3px;
                    }

                    .na_linqC2b {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 210px;
                        position: absolute;
                        top: 170px;
                        width: 20px;
                    }

                    .right_dr {
                        margin: 5px 0 0 52px;
                    }

                    .down_dr {
                        margin-top: 6px;
                    }

                    .left_dr {
                        margin: 5px 52px 0 0;
                    }

                    .na_linqC2c {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 162px;
                        position: absolute;
                        top: 268px;
                        width: 50px;
                    }

                    .na_linqC4a {
                        background: #903 none repeat scroll 0 0;
                        height: 523px;
                        left: 432px;
                        position: absolute;
                        top: 80px;
                        width: 3px;
                    }

                    .na_linqC4b {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 432px;
                        position: absolute;
                        top: 77px;
                        width: 20px;
                    }

                    .na_linqC4c {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 394px;
                        position: absolute;
                        top: 603px;
                        width: 41px;
                    }

                    .na_linqC4d {
                        background: #903 none repeat scroll 0 0;
                        height: 59px;
                        left: 434px;
                        position: absolute;
                        top: 667px;
                        width: 3px;
                    }

                    .na_linqC4e {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 415px;
                        position: absolute;
                        top: 666px;
                        width: 21px;
                    }

                    .na_linqC4f {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 435px;
                        position: absolute;
                        top: 724px;
                        width: 32px;
                    }

                    .na_linqC4g {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 416px;
                        position: absolute;
                        top: 573px;
                        width: 521px;
                    }

                    .na_linqC4h {
                        background: #903 none repeat scroll 0 0;
                        height: 157px;
                        left: 416px;
                        position: absolute;
                        top: 575px;
                        width: 3px;
                    }

                    .na_linqC4l {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 675px;
                        position: absolute;
                        top: 232px;
                        width: 24px;
                    }

                    .na_linqC4j {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 395px;
                        position: absolute;
                        top: 297px;
                        width: 283px;
                    }

                    .na_linqC4k {
                        background: #903 none repeat scroll 0 0;
                        height: 66px;
                        left: 675px;
                        position: absolute;
                        top: 233px;
                        width: 3px;
                    }

                    .na_linqC4l {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 675px;
                        position: absolute;
                        top: 232px;
                        width: 24px;
                    }

                    .na_linqC4m {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 394px;
                        position: absolute;
                        top: 729px;
                        width: 23px;
                    }

                    .na_linqC5a {
                        background: green;
                        height: 200px;
                        left: 548px;
                        position: absolute;
                        top: 388px;
                        width: 3px;
                    }

                    .na_linqC5b {
                        background: #903 none repeat scroll 0 0;
                        height: 90px;
                        left: 594px;
                        position: absolute;
                        top: 374px;
                        width: 3px;
                    }

                    .na_linqC5e {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 594px;
                        position: absolute;
                        top: 464px;
                        width: 322px;
                    }

                    .na_linqC5f {
                        background: #903 none repeat scroll 0 0;
                        height: 19px;
                        left: 916px;
                        position: absolute;
                        top: 464px;
                        width: 3px;
                    }

                    .na_linqC5c {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 451px;
                        position: absolute;
                        top: 265px;
                        width: 18px;
                    }

                    .na_linqC5d {
                        background: #903 none repeat scroll 0 0;
                        height: 340px;
                        left: 448px;
                        position: absolute;
                        top: 265px;
                        width: 3px;
                    }

                    .na_linqC7a {
                        background: green;
                        height: 39px;
                        left: 783px;
                        position: absolute;
                        top: 560px;
                        width: 3px;
                    }

                    .na_linqC8a {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 867px;
                        position: absolute;
                        top: 325px;
                        width: 51px;
                    }

                    .na_linqC8b {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 866px;
                        position: absolute;
                        top: 790px;
                        width: 40px;
                    }

                    .na_linqC8c {
                        background: #903 none repeat scroll 0 0;
                        height: 437px;
                        left: 905px;
                        position: absolute;
                        top: 356px;
                        width: 3px;
                    }

                    .na_linqC8d {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 905px;
                        position: absolute;
                        top: 356px;
                        width: 26px;
                    }

                    .na_linqC8e {
                        background: #903 none repeat scroll 0 0;
                        height: 281px;
                        left: 887px;
                        position: absolute;
                        top: 418px;
                        width: 3px;
                    }

                    .na_linqC8f {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 887px;
                        position: absolute;
                        top: 418px;
                        width: 48px;
                    }

                    .na_linqC8g {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 864px;
                        position: absolute;
                        top: 696px;
                        width: 24px;
                    }

                    /*.na_linqC8i {
                        background: #903 none repeat scroll 0 0;
                        height: 99px;
                        left: 945px;
                        position: absolute;
                        top: 418px;
                        width: 3px;
                    }*/

                    .na_linqC8j {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 865px;
                        position: absolute;
                        top: 514px;
                        width: 22px;
                    }
                     
                </style>


                <div id="na_wrawpr">

                    <div class="na_linqC1a"></div>

                    <div class="na_linqC2a"></div>

                    <div class="na_linqC2b"></div>

                    <div class="na_linqC2c"></div>

                    <div class="na_linqC4a"></div>
                    <div class="na_linqC4b"></div>
                    <div class="na_linqC4c"></div>
                    <div class="na_linqC20a"></div>


                    <div class="na_linqC4d"></div>
                    <div class="na_linqC4e"></div>
                    <div class="na_linqC4f"></div>

                    <div class="na_linqC4g"></div>
                    <div class="na_linqC4h"></div>
                    <div class="na_linqC4i"></div>
                    <div class="na_linqC4m"></div>

                    <div class="na_linqC4j"></div>
                    <div class="na_linqC4k"></div>
                    <div class="na_linqC4l"></div>

                    <div class="na_linqC7a"></div>



                    <div class="na_linqC5a"></div>

                    <div class="na_linqC5b"></div>
                    <div class="na_linqC5c"></div>
                    <div class="na_linqC5d"></div>

                    <div class="na_linqC5e"></div>
                    <div class="na_linqC5f"></div>

                    <div class="na_linqC8a"></div>

                    <div class="na_linqC8b"></div>
                    <div class="na_linqC8c"></div>
                    <div class="na_linqC8d"></div>

                    <div class="na_linqC8e"></div>
                    <div class="na_linqC8f"></div>
                    <div class="na_linqC8g"></div>

                    <div class="na_linqC8i"></div>
                    <div class="na_linqC8j"></div>
                    <div class="na_linqC8k"></div>

                   


                    <div class="na_first">
                        <ul>
                            <li>
                                <h5><span class="glyph-icon flaticon-home183"></span>Housing ERP</h5>
                            </li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li>
                                <h5><span class="glyph-icon flaticon-building150"></span>Land Procurement</h5>
                                ></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>

                            <a href="<%=this.ResolveUrl("~/")%>">

                                <li><a href="<%=this.ResolveUrl("~/F_01_LPA/PriLandProposal.aspx?Type=Report")%>"><span class="glyph-icon flaticon-tray30 nfal"></span>Initial Land Proposal</a></li>

                                <li></li>
                                <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                <li><a href="#" class="MMenu"><span class="glyph-icon flaticon-light105"></span>Project Feasibility</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_02_Fea/ProjectFeasibility.aspx?Type=fea&prjcode=")%>"><span class="glyph-icon flaticon-wiring"></span>Feasibility Preparation</a></li>
                                <li></li>
                                <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                <li><a href="<%=this.ResolveUrl("~/F_01_LPA/RptAllProTopSheet.aspx?Type=Report&comcod=")%>"><span class="glyph-icon flaticon-wiring"></span>Land Data Bank</a></li>

                                <li></li>
                                <li><h5><span class="glyph-icon flaticon-home183"></span>Interface</h5></li>
                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptPurInterface.aspx")%>"><span class="glyph-icon flaticon-computer214"></span>Construction</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/SubContractorBillInterface.aspx")%>"><span class="glyph-icon flaticon-computer214"></span>Sub-Contractor</a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptPurInterface.aspx?Type=Report&comcod=")%>"><span class="glyph-icon flaticon-computer214"></span>Purchase</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptPurInterface.aspx?Type=Report&comcod=")%>"><span class="glyph-icon flaticon-computer214"></span>Inventory</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptEngInterface.aspx")%>"><span class="glyph-icon flaticon-computer214"></span>General Bill</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_15_DPayReg/BillRegInterface.aspx?Type=Report&comcod=")%>"><span class="glyph-icon flaticon-computer214"></span>Bill Register</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/AccountInterface.aspx?Type=Report&comcod=")%>"><span class="glyph-icon flaticon-computer214"></span>Accounts</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/SalesInterface.aspx?Type=Report&comcod=")%>"><span class="glyph-icon flaticon-computer214"></span>Sales</a></li>
<%--                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/KPIDashboard.aspx")%>"><span class="glyph-icon flaticon-computer214"></span>KPI</a></li>--%>
                                <li><a href="<%=this.ResolveUrl("~/DashboardHRM.aspx")%>"><span class="glyph-icon flaticon-computer214"></span>HRM</a></li>
                                 <li></li>
                               
                                
                                
                        </ul>
                    </div>

                    <div class="na_first2">
                        <ul>

                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa fa-long-arrow-right right_dr  fa-fw"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                        </ul>
                    </div>

                    <div class="na_first">
                        <ul>
                            <li></li>
                            <li></li>
                            <li>
                                <h5><span class="glyph-icon flaticon-budget"></span>Budget</h5>
                            </li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdStdAna.aspx")%>"><span class="glyph-icon flaticon-computer118"></span>Recipe / Analysis </a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_34_Mgt/AccProjectCode.aspx")%>"><span class="glyph-icon flaticon-badges1"></span>Create Project</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/PrjInformation.aspx")%>"><span class="glyph-icon flaticon-information58"></span>Project Information</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdPrjAna.aspx?InputType=BgdMain&prjcode=&sircode=")%>"><span class="glyph-icon flaticon-hammer24"></span>Budget-Construction</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdMaster.aspx?InputType=BgdMain&prjcode=")%>"><span class="glyph-icon flaticon-currency13"></span>Budget-General</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_22_Sal/MktEntryUnit.aspx")%>"><span class="glyph-icon flaticon-sale18"></span>Budget-Sales</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdPrjAna.aspx?InputType=BgdSub&prjcode=&sircode=")%>"><span class="glyph-icon flaticon-selling1"></span>Budget Approval (Lock)</a></li>
                            <li><span class="fa  down_dr fa-long-arrow-down"></span></li>
                            <li><a href="#" class="MMenu"><span class="glyph-icon flaticon-strategical"></span>Planning</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_08_PPlan/ProTargetTimeBasis.aspx?Type=GrpWise&prjcode=&sircode=&flrcod=")%>"><span class="glyph-icon flaticon-seo47"></span>Scheduling</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdLevelRate.aspx?Type=Level")%>"><span class="glyph-icon flaticon-forklift3"></span>Construction Level</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="#" class="MMenu"><span class="glyph-icon flaticon-working9"></span>Construction</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurReqEntry.aspx?InputType=Entry&prjcode=&genno=")%>"><span class="glyph-icon flaticon-construction14"></span>Materials Requisition</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_09_PImp/ImplementPlan.aspx")%>"><span class="glyph-icon flaticon-business73"></span>Work Target</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_09_PImp/PurIssueEntry.aspx?Type=Report&prjcode=")%>"><span class="glyph-icon flaticon-panel3"></span>Work Execution</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_09_PImp/PurLabIssue.aspx?Type=Current&prjcode=&genno=&sircode=")%>"><span class="glyph-icon flaticon-news35"></span>Sub-contractor's R/A bill</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccSubconBillPay.aspx?tcode=99&tname=Payment%20Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-mastercard4"></span>Payment Approval</a></li>

                             <li></li>
                            <li></li>

                            <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>"><span class="glyph-icon flaticon-contract11"></span>All Reports</a></li>
                        </ul>
                    </div>

                    <div class="na_first2">
                        <ul>


                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa left_dr fa-long-arrow-left"></span></li>

                        </ul>
                    </div>

                    <div class="na_first">
                        <ul>
                            <li></li>
                            <li></li>
                            <li>
                                <h5><span class="glyph-icon flaticon-domain2"></span>Procurement</h5>
                            </li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurMktSurvey.aspx?Type=MktSurvey")%>"><span class="glyph-icon flaticon-link30"></span>CS Preparation</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurReqApproval.aspx?Type=RateInput&prjcode=&genno=")%>"><span class="glyph-icon flaticon-currency22"></span>Rate Proposal</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurReqApproval.aspx?Type=Approval&prjcode=&genno=")%>"><span class="glyph-icon flaticon-international36"></span>Requisition Approval</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurAprovEntry.aspx?InputType=PurProposal&genno=")%>"><span class="glyph-icon flaticon-software3"></span>Order Process</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurWrkOrderEntry.aspx?InputType=OrderEntry&genno=")%>"><span class="glyph-icon flaticon-market1"></span>Purchase Order </a></li>
                            <li></li>
                            <li></li>
                            <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurBillEntry.aspx?Type=BillEntry&genno=&sircode=")%>"><span class="glyph-icon flaticon-computerscreen27"></span>Bill Confirmation</a></li>
                            <li><span class="fa fa-long-arrow-up"></span></li>
                            <li></li>
                            <li></li>
                            <li></li>
                            <li></li>
                            <li></li>
                            <li></li>

                            <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurMRREntry.aspx?Type=Entry&prjcode=&genno=&sircode=")%>"><span class="glyph-icon flaticon-research1"></span>Materials Receive</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="#" class="MMenu"><span class="glyph-icon flaticon-house198"></span>Materials Store</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurMatIssue.aspx?Type=Entry")%>"><span class="glyph-icon flaticon-checkmark11"></span>Materials  Issue</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/AccountInterface.aspx?Type=Report")%>"><span class="glyph-icon flaticon-construction12"></span>Materials  Transfer</a></li>

                        </ul>
                    </div>

                    <div class="na_first2">
                        <ul>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa right_dr fa-long-arrow-right"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                        </ul>
                    </div>

                    <div class="na_first">
                        <ul>
                            <li></li>
                            <li></li>
                            <li>
                                <h5><span class="glyph-icon flaticon-black193"></span>Customer Relation</h5>
                            </li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_21_Mkt/RptMktAppointment.aspx?Type=Todaysdis&UType=Mgt")%>"><span class="glyph-icon flaticon-buy12"></span>Today's appointment</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_21_Mkt/ToDaysAppointment.aspx?Type=ClDiscuss&UType=Mgt")%>"><span class="glyph-icon flaticon-searching26"></span>Communication</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurReqApproval.aspx?Type=Approval")%>" class="MMenu"><span class="glyph-icon flaticon-house118"></span>Sales </a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_22_Sal/RptSalInterest.aspx?Type=CustNoteSheet")%>"><span class="glyph-icon flaticon-pencil41"></span>Customer(Note sheet)</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_22_Sal/MktSalsPayment.aspx?Type=Sales")%>"><span class="glyph-icon flaticon-coins18"></span>Payment Schedule</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurBillEntry.aspx?Type=BillEntry&genno=&sircode=")%>" class="MMenu"><span class="glyph-icon flaticon-speechbubbles3"></span>Credit Realization</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_22_Sal/RptSalInterest.aspx?Type=DueCollAll")%>"><span class="glyph-icon flaticon-newspaper21"></span>Invoice</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_23_CR/MktMoneyReceipt.aspx?Type=CustCare")%>"><span class="glyph-icon flaticon-magnifyingglass27"></span>Collection (MR)</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccChqueDeposit.aspx?Type=ChquedepEntry")%>"><span class="glyph-icon flaticon-creditcard7"></span>Cheque Deposit</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccSales.aspx?Type=Entry")%>"><span class="glyph-icon flaticon-data55"></span>Cheque Clearemce</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_22_Sal/RptThanksLetter.aspx?Type=Remind")%>"><span class="glyph-icon flaticon-software7"></span>Reminder</a></li>

                            <li></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="#" class="MMenu"><span class="glyph-icon flaticon-telephone172"></span>Customer Care</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_24_CC/CustMaintenanceWork.aspx?Type=Entry&genno=&Date1=")%>"><span class="glyph-icon flaticon-prize3"></span>Client's Choice</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_24_CC/CustMaintenanceWork.aspx?Type=Entry&genno=&Date1=")%>"><span class="glyph-icon flaticon-sales4"></span>Client's Modification</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4105#")%>" class="MMenu"><span class="glyph-icon flaticon-file294"></span>Registration</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_25_Reg/EntryRegclearacne.aspx")%>"><span class="glyph-icon flaticon-game50"></span>Clearence</a></li>
                            <li></li>
                        </ul>
                    </div>

                    <div class="na_first2">
                        <ul>

                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                            <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                            <li></li>
                            <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                            <li></li>
                            <li><span class="fa right_dr fa-long-arrow-right"></span></li>

                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>

                        </ul>
                    </div>

                    <div class="na_first">
                        <ul>
                            <li></li>
                            <li></li>
                            <li>
                                <h5><span class="glyph-icon flaticon-settings49"></span>Finance & Accounts</h5>
                            </li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_05_Busi/YearlyPlanningBudget.aspx?Type=Yearly")%>" class="MMenu"><span class="glyph-icon flaticon-graph22"></span>Annual Business Plan</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccMonthlyBgd.aspx?Type=All&actcode=&year=")%>"><span class="glyph-icon flaticon-statistics15"></span>Working Budget</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AllVoucherTopSheet.aspx")%>"><span class="glyph-icon flaticon-money13"></span>Payment Voucher</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AllVoucherTopSheet.aspx")%>"><span class="glyph-icon flaticon-money405"></span>Deposit Voucher</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AllVoucherTopSheet.aspx")%>"><span class="glyph-icon flaticon-prize3"></span>Journal Voucher</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccInterComVoucher.aspx")%>"><span class="glyph-icon flaticon-computer218"></span>Inter Company Payment</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/AccountInterface.aspx?Type=Report")%>"><span class="glyph-icon flaticon-register"></span>Update- Sales (Adv.)</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/AccountInterface.aspx?Type=Report")%>"><span class="glyph-icon flaticon-stocks3"></span>Update-Collection</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_23_CR/RptCustPayStatus.aspx?Type=ClLedger")%>"><span class="glyph-icon flaticon-money296"></span>Hit Client Ledger</a></li>
                            <li></li>
                            <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/AccountInterface.aspx?Type=Report")%>"><span class="glyph-icon flaticon-bars-graphic"></span>Update-Purchase</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/SuplierPayment.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-creditcard7"></span>Supplier's payment</a></li>
                            <li></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccSubconBillPay.aspx?tcode=99&tname=Payment%20Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-data62"></span>Contractor's Payment</a></li>
                            <li></li>
                           <%-- <li><a href="#" class="MMenu"><span class="glyph-icon flaticon-credit56"></span>Digital Payment</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_15_DPayReg/AccOnlinePaymnt.aspx")%>"><span class="glyph-icon flaticon-business73"></span>Bill entry / update</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_15_DPayReg/AccOnlinePaymentRa.aspx?Type=ChequeReady")%>"><span class="glyph-icon flaticon-report1"></span>Forward</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_15_DPayReg/AccOnlinePaymentApp.aspx?Type=ChequePayment")%>"><span class="glyph-icon flaticon-favourite26"></span>Approval</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_15_DPayReg/ChequeSignSheet.aspx?Type=Acc")%>"><span class="glyph-icon flaticon-pencil41"></span>Cheque Issue</a></li>--%>

                             <li><h5><span class="glyph-icon flaticon-home183"></span>Dashborad</h5></li>
                                <li><a href="<%=this.ResolveUrl("~/F_22_Sal/SalesInformation.aspx")%>"><span class="glyph-icon flaticon-sales3"></span>Sales</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurInformation.aspx?Type=Report&comcod=")%>"><span class="glyph-icon flaticon-favourite26"></span>Purchase</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_18_MAcc/AccDashBoard.aspx")%>"><span class="glyph-icon flaticon-settings49"></span>Accounts</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_32_Mis/RptConstruProgress.aspx")%>"><span class="glyph-icon flaticon-working9"></span>Construction</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_32_Mis/RptMisMasterBgd.aspx?Type=InvPlan")%>"><span class="glyph-icon flaticon-working9"></span>Project Report</a></li>

                        </ul>
                    </div>



                </div>

            </asp:Panel>
        </div>

         <div class="row">
            <asp:Panel ID="pnlflochartCon" runat="server" CssClass="pnlflowchart" Visible="false">

                <style>
                    * {
                        margin: 0;
                        padding: 0;
                    }

                    .pnlflowchart {
                        overflow: hidden;
                        background-image: url(Image/bg.PNG) !important;
                    }

                    #na_wrawpr {
                        width: 1100px;
                        height: auto;
                        margin: 0 auto;
                        position: relative;
                        background-image: url(image/bg.PNG);
                        /*background-color: #F7F7F7;
                            background-image: linear-gradient(90deg, rgba(255,255,255,.07) 50%, transparent 50%),
                            linear-gradient(90deg, rgba(255,255,255,.13) 50%, transparent 50%),
                            linear-gradient(90deg, transparent 50%, rgba(255,255,255,.17) 50%),
                            linear-gradient(90deg, transparent 50%, rgba(255,255,255,.19) 50%);
                            background-size: 13px, 29px, 37px, 53px;*/
                    }

                    .na_first {
                        width: 160px;
                        float: left;
                    }

                    #na_wrawpr ul {
                        margin: 0;
                        padding: 0;
                    }

                        #na_wrawpr ul li {
                            list-style: none;
                            margin: 1px 0;
                            text-align: center;
                            height: 30px;
                        }

                    .na_first ul li h5 {
                        background: #f0fee6 none repeat scroll 0 0 !important;
                        border: 1px solid #699f44;
                        box-shadow: 0 0 4px 2px #bec9b6 inset;
                        font-family: "ar_cenaregular";
                        font-size: 17px;
                        font-weight: normal;
                        line-height: 28px;
                        text-align: center;
                        vertical-align: middle;
                        margin: 0;
                        color: #000;
                        padding: 0;
                    }

                    .na_first ul li a.MMenu {
                        background: #f0fee6 none repeat scroll 0 0 !important;
                        border: 1px solid #699f44;
                        box-shadow: 0 0 4px 2px #bec9b6 inset;
                        font-family: "ar_cenaregular";
                        font-size: 17px;
                        font-weight: normal;
                        line-height: 28px;
                        text-align: center;
                        vertical-align: middle;
                        margin: 0;
                        color: #000;
                        padding: 0;
                    }

                    .na_first ul li a {
                        background: rgba(0, 0, 0, 0) linear-gradient(to bottom, #ffffff 0%, #fbfbfb 49%, #d2d2d2 98%, #cce0f2 100%) repeat scroll 0 0;
                        font-family: "ar_cenaregular";
                        font-size: 14px;
                        font-weight: normal;
                        line-height: 28px;
                        display: block;
                        color: #000;
                        text-align: center;
                        vertical-align: middle;
                        border: 1px solid #999;
                        margin: 0;
                        padding: 0;
                        text-decoration: none;
                    }

                    .na_first2 {
                        width: 75px;
                        float: left;
                    }

                        .na_first2 ul li span {
                            height: 30px;
                            font-size: 20px;
                            color: #903;
                        }

                        .na_first2 ul li i {
                            height: 30px;
                            font-size: 20px;
                            color: #903;
                        }

                    .na_first ul li span {
                        height: 30px;
                        font-size: 20px;
                        color: green;
                    }

                    .na_first ul li i {
                        height: 30px;
                        font-size: 20px;
                        color: #903;
                    }

                    [class^="flaticon-"]::before, [class*=" flaticon-"]::before, [class^="flaticon-"]::after, [class*=" flaticon-"]::after {
                        font-size: 20px !important;
                        margin: 0;
                        padding: 0 10px 0 2px !important;
                        width: 20px !important;
                    }


                    .na_first ul a {
                        text-align: left !important;
                    }

                    .na_first ul h5 {
                        text-align: left !important;
                    }

                    .na_linqC1a {
                        background: green;
                        height: 50px;
                        left: 78px;
                        position: absolute;
                        top: 160px;
                        width: 3px;
                    }

                    .na_linqC2a {
                        background: #903 none repeat scroll 0 0;
                        height: 98px;
                        left: 209px;
                        position: absolute;
                        top: 170px;
                        width: 3px;
                    }

                    .na_linqC20a {
                        background: green;
                        height: 50px;
                        left: 78px;
                        position: absolute;
                        top: 282px;
                        width: 3px;
                    }

                    .na_linqC2b {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 210px;
                        position: absolute;
                        top: 170px;
                        width: 20px;
                    }

                    .right_dr {
                        margin: 5px 0 0 52px;
                    }

                    .down_dr {
                        margin-top: 6px;
                    }

                    .left_dr {
                        margin: 5px 52px 0 0;
                    }

                    .na_linqC2c {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 162px;
                        position: absolute;
                        top: 268px;
                        width: 50px;
                    }

                    .na_linqC4a {
                        background: #903 none repeat scroll 0 0;
                        height: 523px;
                        left: 432px;
                        position: absolute;
                        top: 80px;
                        width: 3px;
                    }

                    .na_linqC4b {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 432px;
                        position: absolute;
                        top: 77px;
                        width: 20px;
                    }

                    .na_linqC4c {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 394px;
                        position: absolute;
                        top: 603px;
                        width: 41px;
                    }

                    .na_linqC4d {
                        background: #903 none repeat scroll 0 0;
                        height: 59px;
                        left: 434px;
                        position: absolute;
                        top: 667px;
                        width: 3px;
                    }

                    .na_linqC4e {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 415px;
                        position: absolute;
                        top: 666px;
                        width: 21px;
                    }

                    .na_linqC4f {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 435px;
                        position: absolute;
                        top: 724px;
                        width: 32px;
                    }

                    .na_linqC4g {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 416px;
                        position: absolute;
                        top: 573px;
                        width: 521px;
                    }

                    .na_linqC4h {
                        background: #903 none repeat scroll 0 0;
                        height: 157px;
                        left: 416px;
                        position: absolute;
                        top: 575px;
                        width: 3px;
                    }

                    .na_linqC4l {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 675px;
                        position: absolute;
                        top: 232px;
                        width: 24px;
                    }

                    .na_linqC4j {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 395px;
                        position: absolute;
                        top: 297px;
                        width: 283px;
                    }

                    .na_linqC4k {
                        background: #903 none repeat scroll 0 0;
                        height: 66px;
                        left: 675px;
                        position: absolute;
                        top: 233px;
                        width: 3px;
                    }

                    .na_linqC4l {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 675px;
                        position: absolute;
                        top: 232px;
                        width: 24px;
                    }

                    .na_linqC4m {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 394px;
                        position: absolute;
                        top: 729px;
                        width: 23px;
                    }

                    .na_linqC5a {
                        background: green;
                        height: 200px;
                        left: 548px;
                        position: absolute;
                        top: 388px;
                        width: 3px;
                    }

                    .na_linqC5b {
                        background: #903 none repeat scroll 0 0;
                        height: 90px;
                        left: 594px;
                        position: absolute;
                        top: 374px;
                        width: 3px;
                    }

                    .na_linqC5e {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 594px;
                        position: absolute;
                        top: 464px;
                        width: 322px;
                    }

                    .na_linqC5f {
                        background: #903 none repeat scroll 0 0;
                        height: 19px;
                        left: 916px;
                        position: absolute;
                        top: 464px;
                        width: 3px;
                    }

                    .na_linqC5c {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 451px;
                        position: absolute;
                        top: 265px;
                        width: 18px;
                    }

                    .na_linqC5d {
                        background: #903 none repeat scroll 0 0;
                        height: 340px;
                        left: 448px;
                        position: absolute;
                        top: 265px;
                        width: 3px;
                    }

                    .na_linqC7a {
                        background: green;
                        height: 39px;
                        left: 783px;
                        position: absolute;
                        top: 560px;
                        width: 3px;
                    }

                    .na_linqC8a {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 867px;
                        position: absolute;
                        top: 325px;
                        width: 51px;
                    }

                    .na_linqC8b {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 866px;
                        position: absolute;
                        top: 790px;
                        width: 40px;
                    }

                    .na_linqC8c {
                        background: #903 none repeat scroll 0 0;
                        height: 437px;
                        left: 905px;
                        position: absolute;
                        top: 356px;
                        width: 3px;
                    }

                    .na_linqC8d {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 905px;
                        position: absolute;
                        top: 356px;
                        width: 26px;
                    }

                    .na_linqC8e {
                        background: #903 none repeat scroll 0 0;
                        height: 281px;
                        left: 887px;
                        position: absolute;
                        top: 418px;
                        width: 3px;
                    }

                    .na_linqC8f {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 887px;
                        position: absolute;
                        top: 418px;
                        width: 48px;
                    }

                    .na_linqC8g {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 864px;
                        position: absolute;
                        top: 696px;
                        width: 24px;
                    }

                    /*.na_linqC8i {
                        background: #903 none repeat scroll 0 0;
                        height: 99px;
                        left: 945px;
                        position: absolute;
                        top: 418px;
                        width: 3px;
                    }*/

                    .na_linqC8j {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 865px;
                        position: absolute;
                        top: 514px;
                        width: 22px;
                    }

                    
                </style>


                <div id="na_wrawpr">
                    
                    <div class="na_linqC1a"></div>

                    <div class="na_linqC2a"></div>

                    <div class="na_linqC2b"></div>

                    <div class="na_linqC2c"></div>

                    <div class="na_linqC4a"></div>
                    <div class="na_linqC4b"></div>
                    <div class="na_linqC4c"></div>
                   

                    <div class="na_linqC4d"></div>
                    <div class="na_linqC4e"></div>
                    <div class="na_linqC4f"></div>

                    <div class="na_linqC4g"></div>
                    <div class="na_linqC4h"></div>
                    <div class="na_linqC4i"></div>
                    <div class="na_linqC4m"></div>

                    <div class="na_linqC4j"></div>
                    <div class="na_linqC4k"></div>
                    <div class="na_linqC4l"></div>

                   

                    <div class="na_linqC5a"></div>

                    <div class="na_linqC5b"></div>
                    <div class="na_linqC5c"></div>
                    <div class="na_linqC5d"></div>

                    <div class="na_linqC5e"></div>
                    <div class="na_linqC5f"></div>

                    <div class="na_linqC8a"></div>

                    <div class="na_linqC8b"></div>
                    <div class="na_linqC8c"></div>
                    <div class="na_linqC8d"></div>

                    <div class="na_linqC8e"></div>
                    <div class="na_linqC8f"></div>
                    <div class="na_linqC8g"></div>

                    <div class="na_linqC8i"></div>
                    <div class="na_linqC8j"></div>
                    <div class="na_linqC8k"></div>

                   

                    <div class="na_first">
                        <ul>
                            <li>
                                <h5><span class="glyph-icon flaticon-home183"></span>Construction ERP</h5>
                            </li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li>
                                <h5><span class="glyph-icon flaticon-building150"></span>Tender Proposal</h5>
                                ></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>

                            <a href="<%=this.ResolveUrl("~/")%>">

                                <li><a href="<%=this.ResolveUrl("~/F_07_Ten/TASStdAnalysis.aspx")%>"><span class="glyph-icon flaticon-tray30 nfal"></span>Create Standard Analysis</a></li>

                                <li></li>
                                <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                <%--<li><a href="#" class="MMenu"><span class="glyph-icon flaticon-light105"></span>Tender Proposal</a></li>--%>
                                <li><a href="<%=this.ResolveUrl("~/F_07_Ten/TASActAnalysis.aspx?Type=Input")%>"><span class="glyph-icon flaticon-tray30 nfal"></span>Create Tender Proposal</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdMaster.aspx?InputType=BgdMain")%>"><span class="glyph-icon flaticon-tray30 nfal"></span>Other's Cost Input</a></li>
                                
                                <li><a href="<%=this.ResolveUrl("~/F_07_Ten/TasProjectcost.aspx?Type=Margin")%>"><span class="glyph-icon flaticon-tray30 nfal"></span>Margin Input</a></li>

                              <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                <li><a href="<%=this.ResolveUrl("~/F_07_Ten/RptPrjSchAnaLysis.aspx?Type=TenderProposal")%>"><span class="glyph-icon flaticon-wiring"></span>Final Tender Proposal</a></li>

                                <li></li>
                                <li><h5><span class="glyph-icon flaticon-home183"></span>Interface</h5></li>
                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptPurInterface.aspx")%>"><span class="glyph-icon flaticon-computer214"></span>Construction</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptPurInterface.aspx")%>"><span class="glyph-icon flaticon-computer214"></span>Purchase</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/SubContractorBillInterface.aspx")%>"><span class="glyph-icon flaticon-computer214"></span>Sub-Contractor</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptEngInterface.aspx")%>"><span class="glyph-icon flaticon-computer214"></span>Inventroy</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_15_DPayReg/BillRegInterface.aspx")%>"><span class="glyph-icon flaticon-computer214"></span>Billing Mgt</a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptEngInterface.aspx")%>"><span class="glyph-icon flaticon-computer214"></span>General Bill</a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/AccountInterface.aspx")%>"><span class="glyph-icon flaticon-computer214"></span>Accounts</a></li>
                               <%-- <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/SalesInterface.aspx")%>"><span class="glyph-icon flaticon-computer214"></span>Sales</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/KPIDashboard.aspx")%>"><span class="glyph-icon flaticon-computer214"></span>KPI</a></li>--%>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/InterfaceHR.aspx")%>"><span class="glyph-icon flaticon-computer214"></span>HRM</a></li>
                                 <li></li>
                               
                                
                                
                        </ul>
                    </div>

                    <div class="na_first2">
                        <ul>

                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa fa-long-arrow-right right_dr  fa-fw"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                        </ul>
                    </div>

                    <div class="na_first">
                        <ul>
                            <li></li>
                            <li></li>
                            <li>
                                <h5><span class="glyph-icon flaticon-budget"></span>Budget</h5>
                            </li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdStdAna.aspx")%>"><span class="glyph-icon flaticon-computer118"></span>Recipe / Analysis </a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_34_Mgt/AccProjectCode.aspx")%>"><span class="glyph-icon flaticon-badges1"></span>Create Project</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/PrjInformation.aspx")%>"><span class="glyph-icon flaticon-information58"></span>Project Information</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdPrjAna.aspx?InputType=BgdMain")%>"><span class="glyph-icon flaticon-hammer24"></span>Budget-Construction</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdMaster.aspx?InputType=BgdMain")%>"><span class="glyph-icon flaticon-currency13"></span>Budget-General</a></li>
                            <li><%--<a href="<%=this.ResolveUrl("~/F_22_Sal/MktEntryUnit.aspx")%>"><span class="glyph-icon flaticon-sale18"></span>Budget-Sales</a>--%></li>
                            <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdPrjAna.aspx?InputType=BgdSub")%>"><span class="glyph-icon flaticon-selling1"></span>Budget Approval (Lock)</a></li>
                            <li><span class="fa  down_dr fa-long-arrow-down"></span></li>
                            <li><a href="#" class="MMenu"><span class="glyph-icon flaticon-strategical"></span>Planning</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_08_PPlan/ProTargetTimeBasis.aspx?Type=GrpWise")%>"><span class="glyph-icon flaticon-seo47"></span>Scheduling</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdLevelRate.aspx?Type=Level")%>"><span class="glyph-icon flaticon-forklift3"></span>Construction Level</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="#" class="MMenu"><span class="glyph-icon flaticon-working9"></span>Construction</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurReqEntry.aspx?InputType=Entry&prjcode=&genno=")%>"><span class="glyph-icon flaticon-construction14"></span>Materials Requisition</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_09_PImp/ImplementPlan.aspx")%>"><span class="glyph-icon flaticon-business73"></span>Work Target</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_09_PImp/PurIssueEntry.aspx")%>"><span class="glyph-icon flaticon-panel3"></span>Work Execution</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_09_PImp/PurLabIssue.aspx?Type=Current")%>"><span class="glyph-icon flaticon-news35"></span>Sub-contractor's R/A bill</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_09_PImp/SubConBillEntry.aspx")%>"><span class="glyph-icon flaticon-mastercard4"></span>Payment Approval</a></li>

                             <li></li>
                            <li></li>

                            <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>"><span class="glyph-icon flaticon-contract11"></span>All Reports</a></li>
                        </ul>
                    </div>

                    <div class="na_first2">
                        <ul>


                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa left_dr fa-long-arrow-left"></span></li>

                        </ul>
                    </div>

                    <div class="na_first">
                        <ul>
                            <li></li>
                            <li></li>
                            <li>
                                <h5><span class="glyph-icon flaticon-domain2"></span>Procurement</h5>
                            </li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurMktSurvey.aspx?Type=MktSurvey")%>"><span class="glyph-icon flaticon-link30"></span>CS Preparation</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurReqApproval.aspx?Type=RateInput")%>"><span class="glyph-icon flaticon-currency22"></span>Rate Proposal</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurReqApproval.aspx?Type=Approval")%>"><span class="glyph-icon flaticon-international36"></span>Requisition Approval</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurAprovEntry.aspx?InputType=PurProposal")%>"><span class="glyph-icon flaticon-software3"></span>Order Process</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurWrkOrderEntry.aspx?InputType=OrderEntry")%>"><span class="glyph-icon flaticon-market1"></span>Purchase Order </a></li>
                            <li></li>
                            <li></li>
                            <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurBillEntry.aspx?Type=BillEntry")%>"><span class="glyph-icon flaticon-computerscreen27"></span>Bill Confirmation</a></li>
                            <li><span class="fa fa-long-arrow-up"></span></li>
                            <li></li>
                            <li></li>
                            <li></li>
                            <li></li>
                            <li></li>
                            <li></li>

                            <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurMRREntry.aspx?Type=Entry")%>"><span class="glyph-icon flaticon-research1"></span>Materials Receive</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="#" class="MMenu"><span class="glyph-icon flaticon-house198"></span>Materials Store</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurMatIssue.aspx?Type=Entry")%>"><span class="glyph-icon flaticon-checkmark11"></span>Materials  Issue</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_12_Inv/MaterialsTransfer.aspx")%>"><span class="glyph-icon flaticon-construction12"></span>Materials  Transfer</a></li>

                        </ul>
                    </div>

                    <div class="na_first2">
                        <ul>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa right_dr fa-long-arrow-right"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                        </ul>
                    </div>

                    <div class="na_first">
                        <ul>
                            <li></li>
                            <li></li>
                            <li>
                                 
                            </li>
                            <li><%--<span class="fa down_dr fa-long-arrow-down"></span>--%></li>
                            <li><%--<a href="<%=this.ResolveUrl("~/F_21_Mkt/RptMktAppointment.aspx?Type=Todaysdis&UType=Mgt")%>"><span class="glyph-icon flaticon-buy12"></span>Today's appointment</a>--%></li>
                            <li><%--<a href="<%=this.ResolveUrl("~/F_21_Mkt/ToDaysAppointment.aspx?Type=ClDiscuss&UType=Mgt")%>"><span class="glyph-icon flaticon-searching26"></span>Communication</a>--%></li>
                            <li><%--<span class="fa down_dr fa-long-arrow-down"></span>--%></li>
                            <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurReqApproval.aspx?Type=Approval")%>" class="MMenu"><span class="glyph-icon flaticon-house118"></span>Billing Management </a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_16_Bill/BillingRateEntry.aspx")%>"><span class="glyph-icon flaticon-pencil41"></span>Bill Rate Input</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_16_Bill/BillEntry.aspx")%>"><span class="glyph-icon flaticon-coins18"></span>Create Bill</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurBillEntry.aspx?Type=BillEntry")%>" class="MMenu"><span class="glyph-icon flaticon-speechbubbles3"></span>Credit Realization</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_22_Sal/RptSalInterest.aspx?Type=DueCollAll")%>"><span class="glyph-icon flaticon-newspaper21"></span>Invoice</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_23_CR/CustOthMoneyReceipt.aspx?Type=CustCare")%>"><span class="glyph-icon flaticon-magnifyingglass27"></span>Collection (MR)</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccChqueDeposit.aspx?Type=ChquedepEntry")%>"><span class="glyph-icon flaticon-creditcard7"></span>Cheque Deposit</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccSales.aspx")%>"><span class="glyph-icon flaticon-data55"></span>Cheque Clearemce</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_22_Sal/RptThanksLetter.aspx?Type=Remind")%>"><span class="glyph-icon flaticon-software7"></span>Reminder</a></li>

                          <%--  <li></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="#" class="MMenu"><span class="glyph-icon flaticon-telephone172"></span>Customer Care</a></li>
                            <li><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4105#")%>"><span class="glyph-icon flaticon-prize3"></span>Correspondance</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_24_CC/CustMaintenanceWork.aspx")%>"><span class="glyph-icon flaticon-sales4"></span>Client's Modification</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4105#")%>" class="MMenu"><span class="glyph-icon flaticon-file294"></span>Registration</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_25_Reg/EntryRegclearacne.aspx")%>"><span class="glyph-icon flaticon-game50"></span>Clearence</a></li>
                            <li></li>--%>
                        </ul>
                    </div>

                    <div class="na_first2">
                        <ul>

                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                            <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                            <li></li>
                            <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                            <li></li>
                            <li><span class="fa right_dr fa-long-arrow-right"></span></li>

                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>

                        </ul>
                    </div>

                    <div class="na_first">
                        <ul>
                            <li></li>
                            <li></li>
                            <li>
                                <h5><span class="glyph-icon flaticon-settings49"></span>Finance & Accounts</h5>
                            </li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_05_Busi/YearlyPlanningBudget.aspx?Type=Yearly")%>" class="MMenu"><span class="glyph-icon flaticon-graph22"></span>Annual Business Plan</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccMonthlyBgd.aspx")%>"><span class="glyph-icon flaticon-statistics15"></span>Working Budget</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/GeneralAccounts.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-money13"></span>Payment Voucher</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/GeneralAccounts.aspx?tcode=99&tname=Deposit Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-money405"></span>Deposit Voucher</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/GeneralAccounts.aspx?tcode=99&tname=Journal Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-prize3"></span>Journal Voucher</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccInterComVoucher.aspx")%>"><span class="glyph-icon flaticon-computer218"></span>Inter Company Payment</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccSalJournal.aspx?Type=Consolidate")%>"><span class="glyph-icon flaticon-register"></span>Update- Sales (Adv.)</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccSales.aspx")%>"><span class="glyph-icon flaticon-stocks3"></span>Update-Collection</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_23_CR/RptCustPayStatus.aspx?Type=ClLedger")%>"><span class="glyph-icon flaticon-money296"></span>Hit Client Ledger</a></li>
                            <li></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccPurchase.aspx")%>"><span class="glyph-icon flaticon-bars-graphic"></span>Update-Purchase</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/SuplierPayment.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-creditcard7"></span>Supplier's payment</a></li>
                            <li></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/GeneralAccounts.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-data62"></span>Contractor's Payment</a></li>
                            <li></li>
                           <%-- <li><a href="#" class="MMenu"><span class="glyph-icon flaticon-credit56"></span>Digital Payment</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_15_DPayReg/AccOnlinePaymnt.aspx")%>"><span class="glyph-icon flaticon-business73"></span>Bill entry / update</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_15_DPayReg/AccOnlinePaymentRa.aspx?Type=ChequeReady")%>"><span class="glyph-icon flaticon-report1"></span>Forward</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_15_DPayReg/AccOnlinePaymentApp.aspx?Type=ChequePayment")%>"><span class="glyph-icon flaticon-favourite26"></span>Approval</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_15_DPayReg/ChequeSignSheet.aspx?Type=Acc")%>"><span class="glyph-icon flaticon-pencil41"></span>Cheque Issue</a></li>--%>

                             <li><h5><span class="glyph-icon flaticon-home183"></span>Dashborad</h5></li>
                                <li><a href="<%=this.ResolveUrl("~/F_22_Sal/SalesInformation.aspx")%>"><span class="glyph-icon flaticon-sales3"></span>Sales</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurInformation.aspx")%>"><span class="glyph-icon flaticon-favourite26"></span>Purchase</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_18_MAcc/AccDashBoard.aspx")%>"><span class="glyph-icon flaticon-settings49"></span>Accounts</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_32_Mis/RptConstruProgress.aspx")%>"><span class="glyph-icon flaticon-working9"></span>Construction</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_32_Mis/RptMisMasterBgd.aspx?Type=InvPlan")%>"><span class="glyph-icon flaticon-working9"></span>Project Report</a></li>

                        </ul>
                    </div>



                </div>

            </asp:Panel>
        </div>

        <div class="row">
            <asp:Panel ID="pnlflochartLand" runat="server" CssClass="pnlflowchart" Visible="false">

                <style>
                    * {
                        margin: 0;
                        padding: 0;
                    }

                    .pnlflowchart {
                        overflow: hidden;
                        background-image: url(Image/bg.PNG) !important;
                    }

                    #na_wrawpr {
                        width: 1100px;
                        height: auto;
                        margin: 0 auto;
                        position: relative;
                        background-image: url(image/bg.PNG);
                        /*background-color: #F7F7F7;
                            background-image: linear-gradient(90deg, rgba(255,255,255,.07) 50%, transparent 50%),
                            linear-gradient(90deg, rgba(255,255,255,.13) 50%, transparent 50%),
                            linear-gradient(90deg, transparent 50%, rgba(255,255,255,.17) 50%),
                            linear-gradient(90deg, transparent 50%, rgba(255,255,255,.19) 50%);
                            background-size: 13px, 29px, 37px, 53px;*/
                    }

                    .na_first {
                        width: 160px;
                        float: left;
                    }

                    #na_wrawpr ul {
                        margin: 0;
                        padding: 0;
                    }

                        #na_wrawpr ul li {
                            list-style: none;
                            margin: 1px 0;
                            text-align: center;
                            height: 30px;
                        }

                    .na_first ul li h5 {
                        background: #f0fee6 none repeat scroll 0 0 !important;
                        border: 1px solid #699f44;
                        box-shadow: 0 0 4px 2px #bec9b6 inset;
                        font-family: "ar_cenaregular";
                        font-size: 17px;
                        font-weight: normal;
                        line-height: 28px;
                        text-align: center;
                        vertical-align: middle;
                        margin: 0;
                        color: #000;
                        padding: 0;
                    }

                    .na_first ul li a.MMenu {
                        background: #f0fee6 none repeat scroll 0 0 !important;
                        border: 1px solid #699f44;
                        box-shadow: 0 0 4px 2px #bec9b6 inset;
                        font-family: "ar_cenaregular";
                        font-size: 17px;
                        font-weight: normal;
                        line-height: 28px;
                        text-align: center;
                        vertical-align: middle;
                        margin: 0;
                        color: #000;
                        padding: 0;
                    }

                    .na_first ul li a {
                        background: rgba(0, 0, 0, 0) linear-gradient(to bottom, #ffffff 0%, #fbfbfb 49%, #d2d2d2 98%, #cce0f2 100%) repeat scroll 0 0;
                        font-family: "ar_cenaregular";
                        font-size: 14px;
                        font-weight: normal;
                        line-height: 28px;
                        display: block;
                        color: #000;
                        text-align: center;
                        vertical-align: middle;
                        border: 1px solid #999;
                        margin: 0;
                        padding: 0;
                        text-decoration: none;
                    }

                    .na_first2 {
                        width: 75px;
                        float: left;
                    }

                        .na_first2 ul li span {
                            height: 30px;
                            font-size: 20px;
                            color: #903;
                        }

                        .na_first2 ul li i {
                            height: 30px;
                            font-size: 20px;
                            color: #903;
                        }

                    .na_first ul li span {
                        height: 30px;
                        font-size: 20px;
                        color: green;
                    }

                    .na_first ul li i {
                        height: 30px;
                        font-size: 20px;
                        color: #903;
                    }

                    [class^="flaticon-"]::before, [class*=" flaticon-"]::before, [class^="flaticon-"]::after, [class*=" flaticon-"]::after {
                        font-size: 20px !important;
                        margin: 0;
                        padding: 0 10px 0 2px !important;
                        width: 20px !important;
                    }


                    .na_first ul a {
                        text-align: left !important;
                    }

                    .na_first ul h5 {
                        text-align: left !important;
                    }

                    .na_linqC1a {
                        background: green;
                        height: 50px;
                        left: 78px;
                        position: absolute;
                        top: 160px;
                        width: 3px;
                    }

                    .na_linqC2a {
                        background: #903 none repeat scroll 0 0;
                        height: 98px;
                        left: 209px;
                        position: absolute;
                        top: 170px;
                        width: 3px;
                    }

                    .na_linqC20a {
                        background: green;
                        height: 50px;
                        left: 78px;
                        position: absolute;
                        top: 282px;
                        width: 3px;
                    }

                    .na_linqC2b {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 210px;
                        position: absolute;
                        top: 170px;
                        width: 20px;
                    }

                    .right_dr {
                        margin: 5px 0 0 52px;
                    }

                    .down_dr {
                        margin-top: 6px;
                    }

                    .left_dr {
                        margin: 5px 52px 0 0;
                    }

                    .na_linqC2c {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 162px;
                        position: absolute;
                        top: 268px;
                        width: 50px;
                    }

                    .na_linqC4a {
                        background: #903 none repeat scroll 0 0;
                        height: 523px;
                        left: 432px;
                        position: absolute;
                        top: 80px;
                        width: 3px;
                    }

                    .na_linqC4b {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 432px;
                        position: absolute;
                        top: 77px;
                        width: 20px;
                    }

                    .na_linqC4c {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 394px;
                        position: absolute;
                        top: 603px;
                        width: 41px;
                    }

                    .na_linqC4d {
                        background: #903 none repeat scroll 0 0;
                        height: 59px;
                        left: 434px;
                        position: absolute;
                        top: 667px;
                        width: 3px;
                    }

                    .na_linqC4e {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 415px;
                        position: absolute;
                        top: 666px;
                        width: 21px;
                    }

                    .na_linqC4f {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 435px;
                        position: absolute;
                        top: 724px;
                        width: 32px;
                    }

                    .na_linqC4g {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 416px;
                        position: absolute;
                        top: 573px;
                        width: 521px;
                    }

                    .na_linqC4h {
                        background: #903 none repeat scroll 0 0;
                        height: 157px;
                        left: 416px;
                        position: absolute;
                        top: 575px;
                        width: 3px;
                    }

                    .na_linqC4l {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 675px;
                        position: absolute;
                        top: 232px;
                        width: 24px;
                    }

                    .na_linqC4j {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 395px;
                        position: absolute;
                        top: 297px;
                        width: 283px;
                    }

                    .na_linqC4k {
                        background: #903 none repeat scroll 0 0;
                        height: 66px;
                        left: 675px;
                        position: absolute;
                        top: 233px;
                        width: 3px;
                    }

                    .na_linqC4l {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 675px;
                        position: absolute;
                        top: 232px;
                        width: 24px;
                    }

                    .na_linqC4m {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 394px;
                        position: absolute;
                        top: 729px;
                        width: 23px;
                    }

                    .na_linqC5a {
                        background: green;
                        height: 200px;
                        left: 548px;
                        position: absolute;
                        top: 388px;
                        width: 3px;
                    }

                    .na_linqC5b {
                        background: #903 none repeat scroll 0 0;
                        height: 90px;
                        left: 594px;
                        position: absolute;
                        top: 374px;
                        width: 3px;
                    }

                    .na_linqC5e {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 594px;
                        position: absolute;
                        top: 464px;
                        width: 322px;
                    }

                    .na_linqC5f {
                        background: #903 none repeat scroll 0 0;
                        height: 19px;
                        left: 916px;
                        position: absolute;
                        top: 464px;
                        width: 3px;
                    }

                    .na_linqC5c {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 451px;
                        position: absolute;
                        top: 265px;
                        width: 18px;
                    }

                    .na_linqC5d {
                        background: #903 none repeat scroll 0 0;
                        height: 340px;
                        left: 448px;
                        position: absolute;
                        top: 265px;
                        width: 3px;
                    }

                    .na_linqC7a {
                        background: green;
                        height: 39px;
                        left: 783px;
                        position: absolute;
                        top: 560px;
                        width: 3px;
                    }

                    .na_linqC8a {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 867px;
                        position: absolute;
                        top: 325px;
                        width: 51px;
                    }

                    .na_linqC8b {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 866px;
                        position: absolute;
                        top: 790px;
                        width: 40px;
                    }

                    .na_linqC8c {
                        background: #903 none repeat scroll 0 0;
                        height: 437px;
                        left: 905px;
                        position: absolute;
                        top: 356px;
                        width: 3px;
                    }

                    .na_linqC8d {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 905px;
                        position: absolute;
                        top: 356px;
                        width: 26px;
                    }

                    .na_linqC8e {
                        background: #903 none repeat scroll 0 0;
                        height: 281px;
                        left: 887px;
                        position: absolute;
                        top: 418px;
                        width: 3px;
                    }

                    .na_linqC8f {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 887px;
                        position: absolute;
                        top: 418px;
                        width: 48px;
                    }

                    .na_linqC8g {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 864px;
                        position: absolute;
                        top: 696px;
                        width: 24px;
                    }

                    /*.na_linqC8i {
                        background: #903 none repeat scroll 0 0;
                        height: 99px;
                        left: 945px;
                        position: absolute;
                        top: 418px;
                        width: 3px;
                    }*/

                    .na_linqC8j {
                        background: #903 none repeat scroll 0 0;
                        height: 3px;
                        left: 865px;
                        position: absolute;
                        top: 514px;
                        width: 22px;
                    }
 
                </style>


                <div id="na_wrawpr">

                    <div class="na_linqC1a"></div>

                    <div class="na_linqC2a"></div>

                    <div class="na_linqC2b"></div>

                    <div class="na_linqC2c"></div>

                    <div class="na_linqC4a"></div>
                    <div class="na_linqC4b"></div>
                    <div class="na_linqC4c"></div>
                    

                    <div class="na_linqC4d"></div>
                    <div class="na_linqC4e"></div>
                    <div class="na_linqC4f"></div>
                    
                    <div class="na_linqC4g"></div>
                    <div class="na_linqC4h"></div>
                    <div class="na_linqC4i"></div>
                    <div class="na_linqC4m"></div>

                    <div class="na_linqC4j"></div>
                    <div class="na_linqC4k"></div>
                    <div class="na_linqC4l"></div>

                    <div class="na_linqC7a"></div>



                    <div class="na_linqC5a"></div>

                    <div class="na_linqC5b"></div>
                    <div class="na_linqC5c"></div>
                    <div class="na_linqC5d"></div>

                    <div class="na_linqC5e"></div>
                    <div class="na_linqC5f"></div>

                    <div class="na_linqC8a"></div>

                    <div class="na_linqC8b"></div>
                    <div class="na_linqC8c"></div>
                    <div class="na_linqC8d"></div>

                    <div class="na_linqC8e"></div>
                    <div class="na_linqC8f"></div>
                    <div class="na_linqC8g"></div>

                    <div class="na_linqC8i"></div>
                    <div class="na_linqC8j"></div>
                    <div class="na_linqC8k"></div>

              

                    <div class="na_first">
                        <ul>
                            <li>
                                <h5><span class="glyph-icon flaticon-home183"></span>Land Mgt ERP</h5>
                            </li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li>
                                <h5><span class="glyph-icon flaticon-building150"></span>Land Procurement</h5>
                                ></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>

                            <a href="<%=this.ResolveUrl("~/")%>">

                                <li><a href="<%=this.ResolveUrl("~/F_01_LPA/PriLandProposal.aspx?Type=Report")%>"><span class="glyph-icon flaticon-tray30 nfal"></span>Initial Land Survey</a></li>

                                <li></li>
                                <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                <li><a href="#" class="MMenu"><span class="glyph-icon flaticon-light105"></span>Land Doc Upload</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_02_Fea/ProjectFeasibility.aspx?Type=fea&prjcode=")%>"><span class="glyph-icon flaticon-wiring"></span>Land Legal Issue</a></li>
                                                                <li><a href="#" class="MMenu"><span class="glyph-icon flaticon-light105"></span>Map Processing</a></li>
                                                                <li><a href="#" class="MMenu"><span class="glyph-icon flaticon-light105"></span>Land Purchase</a></li>

                                
                                <li><a href="<%=this.ResolveUrl("~/F_01_LPA/RptAllProTopSheet.aspx?Type=Report&comcod=")%>"><span class="glyph-icon flaticon-wiring"></span>Land Data Bank</a></li>

                                <li></li>
                                <li><h5><span class="glyph-icon flaticon-home183"></span>Interface</h5></li>
                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptPurInterface.aspx?Type=Report&comcod=")%>"><span class="glyph-icon flaticon-computer214"></span>Land MGT</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptPurInterface.aspx?Type=Report&comcod=")%>"><span class="glyph-icon flaticon-computer214"></span>Budget</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptPurInterface.aspx?Type=Report&comcod=")%>"><span class="glyph-icon flaticon-computer214"></span>Purchase</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptPurInterface.aspx?Type=Report&comcod=")%>"><span class="glyph-icon flaticon-computer214"></span>Inventory</a></li>
                                
                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/SubContractorBillInterface.aspx")%>"><span class="glyph-icon flaticon-computer214"></span>Sub-Contractor</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/SalesInterface.aspx?Type=Report&comcod=")%>"><span class="glyph-icon flaticon-computer214"></span>Sales</a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptEngInterface.aspx")%>"><span class="glyph-icon flaticon-computer214"></span>General Bill</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_15_DPayReg/BillRegInterface.aspx?Type=Report&comcod=")%>"><span class="glyph-icon flaticon-computer214"></span>Bill Register</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/AccountInterface.aspx?Type=Report&comcod=")%>"><span class="glyph-icon flaticon-computer214"></span>Accounts</a></li>
<%--                                <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/KPIDashboard.aspx")%>"><span class="glyph-icon flaticon-computer214"></span>KPI</a></li>--%>
                                <li><a href="<%=this.ResolveUrl("~/DashboardHRM.aspx")%>"><span class="glyph-icon flaticon-computer214"></span>HRM</a></li>
                                 <li></li>
                               
                                
                                
                        </ul>
                    </div>

                    <div class="na_first2">
                        <ul>

                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa fa-long-arrow-right right_dr  fa-fw"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                        </ul>
                    </div>

                    <div class="na_first">
                        <ul>
                            <li></li>
                            <li></li>
                            <li>
                                <h5><span class="glyph-icon flaticon-budget"></span>Budget</h5>
                            </li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdStdAna.aspx")%>"><span class="glyph-icon flaticon-computer118"></span>Recipe / Analysis </a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_34_Mgt/AccProjectCode.aspx")%>"><span class="glyph-icon flaticon-badges1"></span>Create Project</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/PrjInformation.aspx")%>"><span class="glyph-icon flaticon-information58"></span>Project Information</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdPrjAna.aspx?InputType=BgdMain&prjcode=&sircode=")%>"><span class="glyph-icon flaticon-hammer24"></span>Budget-Construction</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdMaster.aspx?InputType=BgdMain&prjcode=")%>"><span class="glyph-icon flaticon-currency13"></span>Budget-General</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_22_Sal/MktEntryUnit.aspx")%>"><span class="glyph-icon flaticon-sale18"></span>Budget-Sales</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdPrjAna.aspx?InputType=BgdSub&prjcode=&sircode=")%>"><span class="glyph-icon flaticon-selling1"></span>Budget Approval (Lock)</a></li>
                            <li><span class="fa  down_dr fa-long-arrow-down"></span></li>
                            <li><a href="#" class="MMenu"><span class="glyph-icon flaticon-strategical"></span>Planning</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_08_PPlan/ProTargetTimeBasis.aspx?Type=GrpWise&prjcode=&sircode=&flrcod=")%>"><span class="glyph-icon flaticon-seo47"></span>Scheduling</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdLevelRate.aspx?Type=Level")%>"><span class="glyph-icon flaticon-forklift3"></span>Construction Level</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="#" class="MMenu"><span class="glyph-icon flaticon-working9"></span>Construction</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurReqEntry.aspx?InputType=Entry&prjcode=&genno=")%>"><span class="glyph-icon flaticon-construction14"></span>Materials Requisition</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_09_PImp/ImplementPlan.aspx")%>"><span class="glyph-icon flaticon-business73"></span>Work Target</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_09_PImp/PurIssueEntry.aspx?Type=Report&prjcode=")%>"><span class="glyph-icon flaticon-panel3"></span>Work Execution</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_09_PImp/PurLabIssue.aspx?Type=Current&prjcode=&genno=&sircode=")%>"><span class="glyph-icon flaticon-news35"></span>Sub-contractor's R/A bill</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccSubconBillPay.aspx?tcode=99&tname=Payment%20Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-mastercard4"></span>Payment Approval</a></li>

                             <li></li>
                            <li></li>

                            <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>"><span class="glyph-icon flaticon-contract11"></span>All Reports</a></li>
                        </ul>
                    </div>

                    <div class="na_first2">
                        <ul>


                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa left_dr fa-long-arrow-left"></span></li>

                        </ul>
                    </div>

                    <div class="na_first">
                        <ul>
                            <li></li>
                            <li></li>
                            <li>
                                <h5><span class="glyph-icon flaticon-domain2"></span>Procurement</h5>
                            </li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurMktSurvey.aspx?Type=MktSurvey")%>"><span class="glyph-icon flaticon-link30"></span>CS Preparation</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurReqApproval.aspx?Type=RateInput&prjcode=&genno=")%>"><span class="glyph-icon flaticon-currency22"></span>Rate Proposal</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurReqApproval.aspx?Type=Approval&prjcode=&genno=")%>"><span class="glyph-icon flaticon-international36"></span>Requisition Approval</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurAprovEntry.aspx?InputType=PurProposal&genno=")%>"><span class="glyph-icon flaticon-software3"></span>Order Process</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurWrkOrderEntry.aspx?InputType=OrderEntry&genno=")%>"><span class="glyph-icon flaticon-market1"></span>Purchase Order </a></li>
                            <li></li>
                            <li></li>
                            <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurBillEntry.aspx?Type=BillEntry&genno=&sircode=")%>"><span class="glyph-icon flaticon-computerscreen27"></span>Bill Confirmation</a></li>
                            <li><span class="fa fa-long-arrow-up"></span></li>
                            <li></li>
                            <li></li>
                            <li></li>
                            <li></li>
                            <li></li>
                            <li></li>

                            <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurMRREntry.aspx?Type=Entry&prjcode=&genno=&sircode=")%>"><span class="glyph-icon flaticon-research1"></span>Materials Receive</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="#" class="MMenu"><span class="glyph-icon flaticon-house198"></span>Materials Store</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurMatIssue.aspx?Type=Entry")%>"><span class="glyph-icon flaticon-checkmark11"></span>Materials  Issue</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/AccountInterface.aspx?Type=Report")%>"><span class="glyph-icon flaticon-construction12"></span>Materials  Transfer</a></li>

                        </ul>
                    </div>

                    <div class="na_first2">
                        <ul>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa right_dr fa-long-arrow-right"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                        </ul>
                    </div>

                    <div class="na_first">
                        <ul>
                            <li></li>
                            <li></li>
                            <li>
                                <h5><span class="glyph-icon flaticon-black193"></span>Customer Relation</h5>
                            </li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_21_Mkt/RptMktAppointment.aspx?Type=Todaysdis&UType=Mgt")%>"><span class="glyph-icon flaticon-buy12"></span>Today's appointment</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_21_Mkt/ToDaysAppointment.aspx?Type=ClDiscuss&UType=Mgt")%>"><span class="glyph-icon flaticon-searching26"></span>Communication</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurReqApproval.aspx?Type=Approval")%>" class="MMenu"><span class="glyph-icon flaticon-house118"></span>Sales </a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_22_Sal/RptSalInterest.aspx?Type=CustNoteSheet")%>"><span class="glyph-icon flaticon-pencil41"></span>Customer(Note sheet)</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_22_Sal/MktSalsPayment.aspx?Type=Sales")%>"><span class="glyph-icon flaticon-coins18"></span>Payment Schedule</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurBillEntry.aspx?Type=BillEntry&genno=&sircode=")%>" class="MMenu"><span class="glyph-icon flaticon-speechbubbles3"></span>Credit Realization</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_22_Sal/RptSalInterest.aspx?Type=DueCollAll")%>"><span class="glyph-icon flaticon-newspaper21"></span>Invoice</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_23_CR/MktMoneyReceipt.aspx?Type=CustCare")%>"><span class="glyph-icon flaticon-magnifyingglass27"></span>Collection (MR)</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccChqueDeposit.aspx?Type=ChquedepEntry")%>"><span class="glyph-icon flaticon-creditcard7"></span>Cheque Deposit</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccSales.aspx?Type=Entry")%>"><span class="glyph-icon flaticon-data55"></span>Cheque Clearemce</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_22_Sal/RptThanksLetter.aspx?Type=Remind")%>"><span class="glyph-icon flaticon-software7"></span>Reminder</a></li>

                            <li></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="#" class="MMenu"><span class="glyph-icon flaticon-telephone172"></span>Customer Care</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_24_CC/CustMaintenanceWork.aspx?Type=Entry&genno=&Date1=")%>"><span class="glyph-icon flaticon-prize3"></span>Client's Choice</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_24_CC/CustMaintenanceWork.aspx?Type=Entry&genno=&Date1=")%>"><span class="glyph-icon flaticon-sales4"></span>Client's Modification</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4105#")%>" class="MMenu"><span class="glyph-icon flaticon-file294"></span>Registration</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_25_Reg/EntryRegclearacne.aspx")%>"><span class="glyph-icon flaticon-game50"></span>Clearence</a></li>
                            <li></li>
                        </ul>
                    </div>

                    <div class="na_first2">
                        <ul>

                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                            <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                            <li></li>
                            <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                            <li></li>
                            <li><span class="fa right_dr fa-long-arrow-right"></span></li>

                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>

                        </ul>
                    </div>

                    <div class="na_first">
                        <ul>
                            <li></li>
                            <li></li>
                            <li>
                                <h5><span class="glyph-icon flaticon-settings49"></span>Finance & Accounts</h5>
                            </li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_05_Busi/YearlyPlanningBudget.aspx?Type=Yearly")%>" class="MMenu"><span class="glyph-icon flaticon-graph22"></span>Annual Business Plan</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccMonthlyBgd.aspx?Type=All&actcode=&year=")%>"><span class="glyph-icon flaticon-statistics15"></span>Working Budget</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AllVoucherTopSheet.aspx")%>"><span class="glyph-icon flaticon-money13"></span>Payment Voucher</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AllVoucherTopSheet.aspx")%>"><span class="glyph-icon flaticon-money405"></span>Deposit Voucher</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AllVoucherTopSheet.aspx")%>"><span class="glyph-icon flaticon-prize3"></span>Journal Voucher</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccInterComVoucher.aspx")%>"><span class="glyph-icon flaticon-computer218"></span>Inter Company Payment</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/AccountInterface.aspx?Type=Report")%>"><span class="glyph-icon flaticon-register"></span>Update- Sales (Adv.)</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/AccountInterface.aspx?Type=Report")%>"><span class="glyph-icon flaticon-stocks3"></span>Update-Collection</a></li>
                            <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                            <li><a href="<%=this.ResolveUrl("~/F_23_CR/RptCustPayStatus.aspx?Type=ClLedger")%>"><span class="glyph-icon flaticon-money296"></span>Hit Client Ledger</a></li>
                            <li></li>
                            <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/AccountInterface.aspx?Type=Report")%>"><span class="glyph-icon flaticon-bars-graphic"></span>Update-Purchase</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/SuplierPayment.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-creditcard7"></span>Supplier's payment</a></li>
                            <li></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccSubconBillPay.aspx?tcode=99&tname=Payment%20Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-data62"></span>Contractor's Payment</a></li>
                            <li></li>
                           <%-- <li><a href="#" class="MMenu"><span class="glyph-icon flaticon-credit56"></span>Digital Payment</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_15_DPayReg/AccOnlinePaymnt.aspx")%>"><span class="glyph-icon flaticon-business73"></span>Bill entry / update</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_15_DPayReg/AccOnlinePaymentRa.aspx?Type=ChequeReady")%>"><span class="glyph-icon flaticon-report1"></span>Forward</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_15_DPayReg/AccOnlinePaymentApp.aspx?Type=ChequePayment")%>"><span class="glyph-icon flaticon-favourite26"></span>Approval</a></li>
                            <li><a href="<%=this.ResolveUrl("~/F_15_DPayReg/ChequeSignSheet.aspx?Type=Acc")%>"><span class="glyph-icon flaticon-pencil41"></span>Cheque Issue</a></li>--%>

                             <li><h5><span class="glyph-icon flaticon-home183"></span>Dashborad</h5></li>
                                <li><a href="<%=this.ResolveUrl("~/F_22_Sal/SalesInformation.aspx")%>"><span class="glyph-icon flaticon-sales3"></span>Sales</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurInformation.aspx?Type=Report&comcod=")%>"><span class="glyph-icon flaticon-favourite26"></span>Purchase</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_18_MAcc/AccDashBoard.aspx")%>"><span class="glyph-icon flaticon-settings49"></span>Accounts</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_32_Mis/RptConstruProgress.aspx")%>"><span class="glyph-icon flaticon-working9"></span>Construction</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_32_Mis/RptMisMasterBgd.aspx?Type=InvPlan")%>"><span class="glyph-icon flaticon-working9"></span>Project Report</a></li>

                        </ul>
                    </div>



                </div>

            </asp:Panel>
        </div>

    </div>

</asp:Content>


