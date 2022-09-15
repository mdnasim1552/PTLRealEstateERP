<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="HRDailyAttenUpload.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_83_Att.HRDailyAttenUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        div#ContentPlaceHolder1_ddlCompany_chzn {
            width: 100%;
        }

        div#ContentPlaceHolder1_ddlDepartment_chzn {
            width: 100% !important;
        }

        .mt20 {
            margin-top: 20px;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

          <%--  $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('#<%=this.gbattn.ClientID %>').tblScrollable();


            gbattn.Scrollable();
            $('.chzn-select').chosen({ search_contains: true });--%>



        }

    </script>

  

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
    <div class="card mt-5">
        <div class="card-header">
            <div class="row">
                <div class="col-lg-1">
                    <div class="form-group">
                        <asp:Label ID="lbldateto" runat="server" Text="Date"></asp:Label>
                        <asp:TextBox ID="txtMrrDate" runat="server" CssClass="form-control form-control-sm pd4"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalExt1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtMrrDate"></cc1:CalendarExtender>
                    </div>
                </div>

                <div class="col-lg-4">
                    <div class="row">
                        <div class="col-lg-10">
                            <asp:Label ID="Label2" runat="server" Text="Upload File "></asp:Label>
                            <input id="File1" runat="server"  name="File1" type="file" class="form-control" />
                        </div>
                        <div class="col-lg-2">
                            <asp:LinkButton ID="CmdUpload" runat="server" CssClass="btn btn-primary btn-sm mt-4" OnClick="UploadFile" TabIndex="4">UpLoad Files</asp:LinkButton>
                        </div>

                    </div>
                </div>


               
                <div class="col-lg-2">
                    <div class="form-group">
                        <asp:Label ID="lblPage" runat="server" Text="Page Size"></asp:Label>

                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" form-control form-control-sm"
                            Font-Bold="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="100">100</asp:ListItem>
                            <asp:ListItem Value="150">150</asp:ListItem>
                            <asp:ListItem Value="200">200</asp:ListItem>
                            <asp:ListItem Value="300">300</asp:ListItem>
                            <asp:ListItem Value="400">400</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

               
                <div class="col-lg-1">
                    <div class="form-group">
                        <asp:LinkButton ID="lbtnShowData" runat="server" CssClass=" margin5px btn btn-primary btn-sm mt20" OnClick="lbtnShowData_Click" TabIndex="5">Show</asp:LinkButton>

                    </div>
                </div>

                 <div class="col-lg-3">
                    <asp:Panel ID="panelexcel" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-lg-7">
                                <asp:Panel ID="pnlxcel" runat="server">
                                    <asp:Label ID="lblExel" runat="server" Text="Excel"></asp:Label>
                                    <div class="uploadFile">
                                        <asp:FileUpload ID="fileuploadExcel" runat="server" onchange="submitform();" />
                                    </div>

                                </asp:Panel>
                            </div>
                            <div class="col-lg-5">
                                <asp:LinkButton ID="btnexcuplosd" runat="server" OnClick="btnexcuplosd_Click"
                                    CssClass=" btn btn-danger btn-sm mt20" Text="Upload Excel"></asp:LinkButton>
                            </div>
                        </div>
                    </asp:Panel>
                </div>

                 <div class="col-lg-2">
                    <asp:CheckBox ID="chktype" runat="server" Visible="false" TabIndex="6" Text="New Machine" CssClass="btn btn-outline-default btn-sm mt20" />
                </div>

            </div>
        </div>
        <div class="card-body">
            <asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label>






            <asp:Label ID="Label4" runat="server" Text="Label" Visible="False"></asp:Label>



            <asp:LinkButton ID="lnkbtaUpLocalpc" runat="server" Visible="false"
                OnClick="lnkbtaUpLocalpc_Click" BackColor="#003366" BorderColor="White"
                ForeColor="White">Upload Text File</asp:LinkButton>

            <asp:LinkButton ID="lbtnTranfered" runat="server" BackColor="#003366" Visible="false"
                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Size="12px"
                ForeColor="White" OnClick="lbtnTranfered_Click">Transfered</asp:LinkButton>


            <asp:GridView ID="gvDailyAttn" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                AllowPaging="True" OnPageIndexChanging="gvDailyAttn_PageIndexChanging">
                <RowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Company & Section Name">
                        <ItemTemplate>
                            <asp:Label ID="lblgvsection" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "comname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "section").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "comname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "section")).Trim(): "")  %>'
                                Width="230px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Emp. ID">
                        <ItemTemplate>
                            <asp:Label ID="lblgvEmpId" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Card #">
                        <ItemTemplate>
                            <asp:Label ID="lblgvEmpIDCard" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                Width="40px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Employee Name & Designation ">
                        <FooterTemplate>
                            <asp:LinkButton ID="lFinalUpdate" runat="server" Font-Bold="True" CssClass="btn btn-danger primaryBtn" OnClick="lFinalUpdate_Click">Final Update</asp:LinkButton>

                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblgvEmpName" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "desig").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")).Trim(): "") %>'
                                Width="180px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Off. Intime">
                        <ItemTemplate>
                            <asp:Label ID="lblgvoffIntime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offintime")).ToString("hh:mm tt") %>'
                                Width="60px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Off. Outtime">
                        <ItemTemplate>
                            <asp:Label ID="lblgvoffouttime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offouttime")).ToString("hh:mm tt") %>'
                                Width="60px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ac. Intime">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvIntime" runat="server" BackColor="Transparent" BorderStyle="None"
                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("hh:mm tt") %>'
                                Width="60px" Font-Size="11px"></asp:TextBox>

                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ac. Outtime">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvOuttime" runat="server" BackColor="Transparent" BorderStyle="None"
                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("hh:mm tt") %>'
                                Width="60px" Font-Size="11px"></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ln Intime" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblgvlnintime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnchintime")).ToString("hh:mm tt") %>'
                                Width="60px" Font-Size="11px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ln Outtime" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblgvlnouttime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnchouttime")).ToString("hh:mm tt") %>'
                                Width="60px" Font-Size="11px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>


                </Columns>
                <FooterStyle CssClass="grvFooter" />
                <EditRowStyle />
                <AlternatingRowStyle />
                <PagerStyle CssClass="gvPagination" />
                <HeaderStyle CssClass="grvHeader" />
            </asp:GridView>

            <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>


            <asp:GridView ID="gbattn" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                AllowPaging="false">
                <RowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo11" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Card #">
                        <ItemTemplate>
                            <asp:Label ID="lblgvEmpIDCard" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                Width="40px"></asp:Label>
                        </ItemTemplate>

                        <FooterTemplate>
                            <asp:LinkButton ID="lFinalUpdatAttn" runat="server" Font-Bold="True" CssClass="btn btn-danger primaryBtn" OnClick="lFinalUpdatAttn_Click">Final Update</asp:LinkButton>

                        </FooterTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Date Time">
                        <ItemTemplate>
                            <asp:Label ID="lblgvoffouttime222" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "adate")).ToString("dd-MMM-yyyy") %>'
                                Width="100px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Time">
                        <ItemTemplate>
                            <asp:Label ID="txtgvIntime" runat="server" BackColor="Transparent" BorderStyle="None"
                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "atime")).ToString("dd-MMM-yyy hh:mm:ss") %>'
                                Width="100px" Font-Size="11px"></asp:Label>

                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Location">
                        <ItemTemplate>
                            <asp:Label ID="lblmachin" runat="server" BackColor="Transparent" BorderStyle="None"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "machid")) %>'
                                Width="60px" Font-Size="11px"></asp:Label>

                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>




                </Columns>
                <FooterStyle CssClass="grvFooter" />
                <EditRowStyle />
                <AlternatingRowStyle />
                <PagerStyle CssClass="gvPagination" />
                <HeaderStyle CssClass="grvHeader" />
            </asp:GridView>

        </div>
    </div>



            </ContentTemplate>
            <Triggers>
            <asp:PostBackTrigger ControlID="CmdUpload" />
        </Triggers>
          </asp:UpdatePanel>

    <%--                <fieldset class="scheduler-border fieldset_A">

                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lbldateto" runat="server" CssClass="lblTxt lblName" Text="Date: "></asp:Label>

                                <asp:TextBox ID="txtMrrDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalExt1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtMrrDate"></cc1:CalendarExtender>
                            </div>
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to" Text="Page Size"></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" smDropDown"
                                    Font-Bold="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                    Width="70px">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                    <asp:ListItem Value="400">400</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-3 pading5px asitCol3 pull-right">
                                <div class="msgHandSt">
                                    <asp:Label ID="lblmsg" CssClass="btn-danger primaryBtn btn disabled" runat="server" Visible="false"></asp:Label>
                                </div>


                            </div>
                        </div>

                        <div class="form group">


                            <asp:Panel ID="panelexcel" runat="server" Visible="false">
                                <div class=" form-group">
                                    <div class="col-sm-3 col-md-5 col-lg-3">
                                        <asp:Panel ID="pnlxcel" runat="server">
                                            <asp:Label ID="lblExel" runat="server" CssClass="lblTxt lblName txtAlgRight" Text="Exele :"></asp:Label>
                                            <div class="uploadFile">
                                                <asp:FileUpload ID="fileuploadExcel" runat="server" onchange="submitform();" />
                                            </div>

                                        </asp:Panel>
                                    </div>
                                    <div class="col-sm-1 col-md-1 col-lg-1">
                                        <asp:LinkButton ID="btnexcuplosd" runat="server" OnClick="btnexcuplosd_Click"
                                            CssClass=" btn btn-danger primarygrdBtn" Text="Upload Exel"></asp:LinkButton>
                                    </div>

                                    <div class="clearfix"></div>
                                </div>



                            </asp:Panel>

                        </div>

                        <div class="form-group">

                            <div class="col-md-3 pading5px">

                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Upload File: "></asp:Label>
                                <input id="File1" runat="server" name="File1" type="file" class="pull-left" />

                                <asp:LinkButton ID="CmdUpload" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="UploadFile" TabIndex="4">UpLoad</asp:LinkButton>
                            

                            </div>
                              <div class="col-md-1 pading5px">
                                      <asp:LinkButton ID="lbtnShowData" runat="server" CssClass=" margin5px btn btn-primary primaryBtn" OnClick="lbtnShowData_Click" TabIndex="5">Show Data</asp:LinkButton>

                                  </div>
                              <div class="col-md-1 pading5px">
                                    <asp:CheckBox ID="chktype" runat="server" TabIndex="6" Text="New Machine" CssClass="btn btn-primary checkBox" />

                                  </div>
                            <div class="col-md-2 pading5px asitCol3">
                                <div class="msgHandSt">



                                </div>
                            </div>
                        </div>

                    </div>
                </fieldset>--%>
</asp:Content>
