
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LetterOfAppoinment.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_81_Rec.LetterOfAppoinment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblpreAdv" runat="server" CssClass="lblTxt lblName">ADV List</asp:Label>
                                        <asp:TextBox ID="txtSrchPre" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindReq" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindReq_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:DropDownList ID="ddlPrevAdvList" runat="server" Width="233" CssClass="form-control inputTxt pull-left" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblPreAdvlist" runat="server" CssClass="dataLblview" Visible="False" Width="285px"></asp:Label>



                                        <asp:Label ID="lblCurDate" runat="server" CssClass=" smLbl_to">Date</asp:Label>
                                        <asp:TextBox ID="txtCurAdvDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurAdvDate_CalendarExtender" runat="server" Format="dd.MM.yyyy" TargetControlID="txtCurAdvDate">
                                        </cc1:CalendarExtender>
                                        <asp:Label ID="lblLastReqNo5" runat="server" Font-Bold="True" CssClass="lblTxt lblName"></asp:Label>

                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblResList" runat="server" CssClass="lblTxt lblName">Post List</asp:Label>
                                        <asp:TextBox ID="txtPostSearch" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindPost" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindPost_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlPOSTList" runat="server" Width="233" CssClass="form-control inputTxt pull-left" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblPostList" runat="server" CssClass="dataLblview" Visible="False" Width="233px"></asp:Label>

                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lbtnOk_Click">ok</asp:LinkButton>
                                        <asp:Label ID="lblmsg1" runat="server" Visible="false" CssClass="btn btn-danger primaryBtn pull-right"></asp:Label>
                                    </div>



                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblCanName" runat="server" CssClass="lblTxt lblName">Candidate Name</asp:Label>
                                        <asp:TextBox ID="txtCanSearch" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindCan" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindCan_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlCanList" runat="server" Width="233" CssClass="form-control inputTxt pull-left" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblCan" runat="server" CssClass="dataLblview" Visible="False" Width="233px"></asp:Label>


                                    </div>



                                </div>


                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:Panel ID="Panel2" runat="server" Visible="False">
                            <table>
                                <tr>
                                    <td class="style16"></td>
                                    <td class="style26" colspan="8">
                                        <asp:TextBox ID="txt0101001" runat="server" BorderWidth="1px" Font-Bold="True" Font-Size="14px"
                                            Height="50px" TextMode="MultiLine" Width="782px">March XX, 2014 
Ref: HRD/LOA/2014/XX   
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16"></td>
                                    <td class="style26" colspan="8">
                                        <asp:TextBox ID="txt0101002" runat="server" BorderWidth="1px" Font-Bold="True" Font-Size="14px"
                                            Height="65px" TextMode="MultiLine" Width="782px">Mr. XXXX 
S/O :  XXXX 
Address: XXX
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16"></td>
                                    <td class="style26" colspan="8">
                                        <asp:TextBox ID="txt0101003" runat="server" BorderWidth="1px" Font-Bold="True" Font-Size="14px"
                                            Height="80px" TextMode="MultiLine" Width="782px">Dear Mr. XXX,

With reference to your application and subsequent interview, the Management is pleased to appoint you as Manager in the department of Accounting under Noname Development Ltd.  with effect from March xxx, 2014 on the following terms and conditions</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16">
                                        <asp:Label ID="Label3" runat="server"  Font-Bold="True" Font-Size="14px"
                                            Height="20px" Style="text-align: right" Width="90px">1.  </asp:Label>
                                    </td>
                                    <td class="style26" colspan="8">
                                        <asp:TextBox ID="txt0102001" runat="server" BorderWidth="1px" Font-Bold="True" Font-Size="14px"
                                            Height="20px" TextMode="MultiLine" Width="782px" Style="text-align: justify">You will be reporting to Mr. XXXX.</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16">
                                        <asp:Label ID="Label2" runat="server"  Font-Bold="True" Font-Size="14px"
                                            Height="80px" Style="text-align: right" Width="90px">2.  </asp:Label>
                                    </td>
                                    <td class="style26" colspan="8">
                                        <asp:TextBox ID="txt0102002" runat="server" BorderWidth="1px" Font-Bold="True" Font-Size="14px"
                                            Height="80px" TextMode="MultiLine" Width="782px" Style="text-align: justify;">You will receive a consolidated salary of Tk. 10,000/- (taka ten thousand only) per month. Please note that individual salary & benefits should be treated with strict confidence. Disclosure of salary & benefits to others is prohibited and against the norms of the company.</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16">
                                        <asp:Label ID="Label1" runat="server"  Font-Bold="True" Font-Size="14px"
                                            Height="50px" Style="text-align: right" Width="90px">3.  </asp:Label>
                                    </td>
                                    <td class="style26" colspan="8">
                                        <asp:TextBox ID="txt0102003" runat="server" BorderWidth="1px" Font-Bold="True" Font-Size="14px"
                                            Height="50px" TextMode="MultiLine" Width="782px">Income tax (if applicable) will be deducted at source as per prevailing Income Tax Law of Bangladesh and you will be responsible for submission of your Annual Income Tax Return.</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16">
                                        <asp:Label ID="TextBox1" runat="server"  Font-Bold="True" Font-Size="14px"
                                            Height="100px" Style="text-align: right" Width="90px">4.  </asp:Label>
                                    </td>
                                    <td class="style26" colspan="8">
                                        <asp:TextBox ID="txt0102004" runat="server" BorderWidth="1px" Font-Bold="True" Font-Size="14px"
                                            Height="100px" TextMode="MultiLine" Width="782px">You will be on an initial “probationary period” of six (6) months. At the end of probation period your work performance will be reviewed and upon successful completion you will become eligible for a confirmed appointment with promotion to next higher grade. In case of unsatisfactory work performance, your probation period may be extended for a further period or your services may be discontinued as deemed appropriate by the management.  </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16">
                                        <asp:Label ID="Label4" runat="server"  Font-Bold="True" Font-Size="14px"
                                            Height="50px" Style="text-align: right" Width="90px">5.  </asp:Label>
                                    </td>
                                    <td class="style26" colspan="8">
                                        <asp:TextBox ID="txt0102005" runat="server" BorderWidth="1px" Font-Bold="True" Font-Size="14px"
                                            Height="50px" TextMode="MultiLine" Width="782px">You will receive the salary break up upon successful completion of probation and subsequent job confirmation.</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16">
                                        <asp:Label ID="Label5" runat="server"  Font-Bold="True" Font-Size="14px"
                                            Height="130px" Style="text-align: right" Width="90px">6.  </asp:Label>
                                    </td>
                                    <td class="style26" colspan="8">
                                        <asp:TextBox ID="txt0102006" runat="server" BorderWidth="1px" Font-Bold="True" Font-Size="14px"
                                            Height="130px" TextMode="MultiLine" Width="782px">Your service during probation period may be terminated by either side by serving 15 (fifteen) days prior notice in writing or paying 15 (fifteen) days salary in lieu of such notice. Your service after confirmation may also be terminated by either side by serving 30 (thirty) days prior notice in writing or paying 30 (thirty) days salary in lieu of such notice. However, in case of negligence of work, misconduct or violation of Company’s rules and regulations by you, the management reserves the right to terminate your service without any prior notice in writing or paying any compensation in lieu of such notice during probation as well as after confirmation of job. </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16">
                                        <asp:Label ID="Label6" runat="server"  Font-Bold="True" Font-Size="14px"
                                            Height="45px" Style="text-align: right" Width="90px">7.  </asp:Label>
                                    </td>
                                    <td class="style26" colspan="8">
                                        <asp:TextBox ID="txt0102007" runat="server" BorderWidth="1px" Font-Bold="True" Font-Size="14px"
                                            Height="45px" TextMode="MultiLine" Width="782px">You will not involve with any part time and/ or full time job / business directly and indirectly during the tenure of your employment with the company.</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16">
                                        <asp:Label ID="Label7" runat="server"  Font-Bold="True" Font-Size="14px"
                                            Height="60px" Style="text-align: right" Width="90px">8.  </asp:Label>
                                    </td>
                                    <td class="style26" colspan="8">
                                        <asp:TextBox ID="txt0102008" runat="server" BorderWidth="1px" Font-Bold="True" Font-Size="14px"
                                            Height="60px" TextMode="MultiLine" Width="782px">You will maintain strict confidentiality & secrecy of all company information and will not divulge the same to any others during the tenure of your service with the company and even after job separation.</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16">
                                        <asp:Label ID="Label8" runat="server"  Font-Bold="True" Font-Size="14px"
                                            Height="45px" Style="text-align: right" Width="90px">9.  </asp:Label>
                                    </td>
                                    <td class="style26" colspan="8">
                                        <asp:TextBox ID="txt0102009" runat="server" BorderWidth="1px" Font-Bold="True" Font-Size="14px"
                                            Height="45px" TextMode="MultiLine" Width="782px">You may be required to serve anywhere in Bangladesh and may be transferred to any other sister concern of Noname Group as per the requirement of the company.</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16">
                                        <asp:Label ID="Label9" runat="server"  Font-Bold="True" Font-Size="14px"
                                            Height="60px" Style="text-align: right" Width="90px">10.  </asp:Label>
                                    </td>
                                    <td class="style26" colspan="8">
                                        <asp:TextBox ID="txt0102010" runat="server" BorderWidth="1px" Font-Bold="True" Font-Size="14px"
                                            Height="60px" TextMode="MultiLine" Width="782px">Your working hours will be according to the office hours fixed by the Management from time to time. Timely and regular attendance will be considered as an important parameter for performance evaluation. </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16">
                                        <asp:Label ID="Label10" runat="server"  Font-Bold="True" Font-Size="14px"
                                            Height="75px" Style="text-align: right" Width="90px">11.  </asp:Label>
                                    </td>
                                    <td class="style26" colspan="8">
                                        <asp:TextBox ID="txt0102011" runat="server" BorderWidth="1px" Font-Bold="True" Font-Size="14px"
                                            Height="75px" TextMode="MultiLine" Width="782px">You will be required to notify your reporting supervisor through any means about absences due to medical or unavoidable reasons on the day of the absence, as soon as possible. Any such absence shall have to be regularized within three working days of your resuming office.</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16">
                                        <asp:Label ID="Label11" runat="server"  Font-Bold="True" Font-Size="14px"
                                            Height="80px" Style="text-align: right" Width="90px">12.  </asp:Label>
                                    </td>
                                    <td class="style26" colspan="8">
                                        <asp:TextBox ID="txt0102012" runat="server" BorderWidth="1px" Font-Bold="True" Font-Size="14px"
                                            Height="80px" TextMode="MultiLine" Width="782px">It is assumed that all the information & certificates which you have provided & mentioned in your resume are all correct and genuine. After your joining if any information is found fake/ manipulated/ untrue, the management reserves the right to terminate your service without any prior notice in writing or paying any compensation in lieu of such notice during probation as well as after confirmation of job. </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16">
                                        <asp:Label ID="Label12" runat="server"  Font-Bold="True" Font-Size="14px"
                                            Height="50px" Style="text-align: right" Width="90px">13.  </asp:Label>
                                    </td>
                                    <td class="style26" colspan="8">
                                        <asp:TextBox ID="txt0102013" runat="server" BorderWidth="1px" Font-Bold="True" Font-Size="14px"
                                            Height="50px" TextMode="MultiLine" Width="782px">Your employment with the company will be governed & regulated as per existing service rules & regulations of the company which may be changed /amended from time to time. </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16">
                                        <asp:Label ID="Label13" runat="server"  Font-Bold="True" Font-Size="14px"
                                            Height="60px" Style="text-align: right" Width="90px">14.  </asp:Label>
                                    </td>
                                    <td class="style26" colspan="8">
                                        <asp:TextBox ID="txt0102014" runat="server" BorderWidth="1px" Font-Bold="True" Font-Size="14px"
                                            Height="60px" TextMode="MultiLine" Width="782px">You are requested to report to Mr. Md.Mostak, Assistant Manager, at Center Point , 8th Floor(Unit-F), 14/A, Tejkunipara, Farmgate, Dhaka-1215 on the date of your joining at 10:00 am for an orientation & placement. </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16"></td>
                                    <td class="style26" colspan="8">
                                        <asp:TextBox ID="txt0201001" runat="server" BorderWidth="1px" Font-Bold="True" Font-Size="14px"
                                            Height="170px" TextMode="MultiLine" Width="782px">If the above terms & conditions are acceptable to you, please sign the duplicate copy of this letter as a token of your acceptance and return to the undersigned.

Thanking You,

_________________
Md. Hafizur Rahaman
Managing Director
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style16"></td>
                                    <td class="style26" colspan="8">
                                        <asp:LinkButton ID="btnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" TabIndex="40" Width="120px"
                                            OnClick="btnUpdate_Click">Update Information</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>

                </div>
            </div>



            <%--  <asp:Panel ID="Panel3" runat="server" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px">
                <table style="width: 900px">
                    <tr>
                        <td class="style78">
                            <asp:Label ID="lblpreAdv" runat="server" CssClass="style15" Font-Bold="True" Font-Size="12px"
                                Height="16px" Style="text-align: left" Text="ADV List:" Width="83px"></asp:Label>
                        </td>
                        <td class="style42">
                            <asp:TextBox ID="txtSrchPre" runat="server" BorderStyle="None" Font-Bold="True" Font-Size="12px"
                                Width="80px" TabIndex="11"></asp:TextBox>
                        </td>
                        <td class="style34" align="right">
                            <asp:ImageButton ID="ImgbtnFindReq" runat="server" Height="19px" ImageUrl="~/Image/find_images.jpg"
                                OnClick="ImgbtnFindReq_Click" Width="16px" TabIndex="12" />
                        </td>
                        <td class="style43">
                            <asp:DropDownList ID="ddlPrevAdvList" runat="server" Font-Size="12px" Width="350px"
                                TabIndex="13">
                            </asp:DropDownList>
                            <asp:Label ID="lblPreAdvlist" runat="server" __designer:wfdid="w4" BackColor="White"
                                Font-Bold="True" Font-Size="14px" ForeColor="Maroon" Style="font-size: 12px;
                                text-align: left" Visible="False" Width="350px"></asp:Label>
                        </td>
                        <td class="style34">
                            <asp:Label ID="lblCurDate" runat="server" CssClass="style15" Font-Bold="True" Font-Size="12px"
                                Height="16px" Style="text-align: right" Text="Date:" Width="40px"></asp:Label>
                        </td>
                        <td class="style34">
                            <asp:TextBox ID="txtCurAdvDate" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Bold="True" Font-Size="12px" TabIndex="3" ToolTip="(dd.mm.yyyy)" Width="80px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCurAdvDate_CalendarExtender" runat="server" Format="dd.MM.yyyy"
                                TargetControlID="txtCurAdvDate">
                            </cc1:CalendarExtender>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td class="style92">
                            &nbsp;
                        </td>
                        <td class="style46">
                            &nbsp;
                        </td>
                        <td class="style19">
                        </td>
                    </tr>
                    <tr>
                        <td class="style78">
                            <asp:Label ID="lblResList" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: left;
                                color: #FFFFFF;" Text="Post List:" Width="98px"></asp:Label>
                        </td>
                        <td class="style42">
                            <asp:TextBox ID="txtPostSearch" runat="server" BorderStyle="None" Font-Bold="True"
                                Font-Size="12px" TabIndex="15" Width="80px"></asp:TextBox>
                        </td>
                        <td align="right" class="style34">
                            <asp:ImageButton ID="ImgbtnFindPost" runat="server" BorderStyle="None" Height="19px"
                                ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindPost_Click" TabIndex="16"
                                Width="16px" />
                        </td>
                        <td class="style43">
                            <asp:DropDownList ID="ddlPOSTList" runat="server" AutoPostBack="True" Font-Size="12px"
                                Style="border-right: midnightblue 1px solid; border-top: midnightblue 1px solid;
                                border-left: midnightblue 1px solid; border-bottom: midnightblue 1px solid; background-color: #fffbf1"
                                TabIndex="17" Width="350px" OnSelectedIndexChanged="ddlPOSTList_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="lblPostList" runat="server" __designer:wfdid="w4" BackColor="White"
                                Font-Bold="True" Font-Size="14px" ForeColor="Maroon" Style="font-size: 12px;
                                text-align: left" Visible="False" Width="350px"></asp:Label>
                        </td>
                        <td align="right" class="style87">
                            <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#003366" BorderColor="White"
                                BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                Height="16px" OnClick="lbtnOk_Click" Style="text-align: center;" TabIndex="4"
                                Width="50px">Ok</asp:LinkButton>
                        </td>
                        <td class="style91">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td class="style92">
                            <asp:Label ID="lblmsg1" runat="server" __designer:wfdid="w4" BackColor="Red" Font-Bold="True"
                                Font-Size="12px" ForeColor="White" Height="18px" Style="font-size: 12px; text-align: left"></asp:Label>
                        </td>
                        <td class="style46">
                            &nbsp;
                        </td>
                        <td class="style19">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style78">
                            <asp:Label ID="lblCanName" runat="server" Font-Bold="True" Font-Size="12px" Style="text-align: left;
                                color: #FFFFFF;" Text="Candidate Name:" Width="98px"></asp:Label>
                        </td>
                        <td class="style42">
                            <asp:TextBox ID="txtCanSearch" runat="server" BorderStyle="None" Font-Bold="True"
                                Font-Size="12px" TabIndex="15" Width="80px"></asp:TextBox>
                        </td>
                        <td align="right" class="style34">
                            <asp:ImageButton ID="ImgbtnFindCan" runat="server" BorderStyle="None" Height="19px"
                                ImageUrl="~/Image/find_images.jpg" OnClick="ImgbtnFindCan_Click" TabIndex="16"
                                Width="16px" />
                        </td>
                        <td class="style43">
                            <asp:DropDownList ID="ddlCanList" runat="server" AutoPostBack="True" Font-Size="12px"
                                Style="border-right: midnightblue 1px solid; border-top: midnightblue 1px solid;
                                border-left: midnightblue 1px solid; border-bottom: midnightblue 1px solid; background-color: #fffbf1"
                                TabIndex="17" Width="350px">
                            </asp:DropDownList>
                            <asp:Label ID="lblCan" runat="server" __designer:wfdid="w4" BackColor="White" Font-Bold="True"
                                Font-Size="14px" ForeColor="Maroon" Style="font-size: 12px; text-align: left"
                                Visible="False" Width="350px"></asp:Label>
                        </td>
                        <td align="right" class="style87">
                            &nbsp;
                        </td>
                        <td class="style91">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td class="style92">
                            &nbsp;
                        </td>
                        <td class="style46">
                            &nbsp;
                        </td>
                        <td class="style19">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>--%>
            <%--<table style="width: 100%; background-color: #C1D2C4;">
                <tr>
                    <td class="style18" colspan="13">
                        
                    </td>
                    <td class="style19">
                        &nbsp;
                    </td>
                </tr>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
